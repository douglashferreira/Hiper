using Hiper.Domain.DTO;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Hiper.Repository.Interfaces;
using Hiper.Domain.DTO.Client;
using Hiper.Domain.Converters;
using System.Diagnostics;

namespace Hiper.Business.RabbitMQ.RabbitMQConsumer
{
    public class RabbitMQConsumer : IRabbitMQConsumer
    {
        private ILogger _logger;
        private IConnection _connection;
        private IModel _channel;
        private const string ClienteQueure = "clientesQueue";
        private readonly IClientRepository _clientRepository;


        public RabbitMQConsumer(ILogger<RabbitMQConsumer> logger, IClientRepository clientRepository)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _logger = logger;
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ClienteQueure, ExchangeType.Direct);
            _clientRepository = clientRepository;
        }

        public async Task ConsumeClientQueue()
        {
            try
            {
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (chanel, evt) =>
                {
                    try
                    {
                        var content = Encoding.UTF8.GetString(evt.Body.ToArray());

                        if (!string.IsNullOrEmpty(content))
                        {
                            List<ClientDTO> clients = JsonSerializer.Deserialize<List<ClientDTO>>(content);
                            SaveClientes(clients).GetAwaiter().GetResult();
                            _channel.BasicAck(evt.DeliveryTag, false);
                        }
                    }

                    catch (System.Exception ex)
                    {
                        _logger.LogError("Erro ao deserializar cliente", ex);
                        _channel.BasicNack(evt.DeliveryTag, false, true);
                    }
                };
                _channel.BasicConsume(ClienteQueure, false, consumer);
            }

            catch (Exception ex)
            {
                _logger.LogError("Erro ao consumir cliente da fila", ex);
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task SaveClientes(List<ClientDTO> clients)
        {
            foreach (var item in clients)
            {
                var add = ClientConverters.ConvertDtoToModel(item);
                _clientRepository.AddAsync(add);
            }
        }
    }
}
