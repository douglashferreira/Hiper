using Hiper.Domain.DTO.Client;
using Hiper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiper.Business.RabbitMQ.RabbitMQSend
{
    public interface IRabbitMQSender
    {
        void SentClientQueue(List<ClientDTO> clients, string queueName);
    }
}
