using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using Hiper.Business.RabbitMQ.RabbitMQConsumer;
using Hiper.Business.RabbitMQ.RabbitMQSend;
using Hiper.Domain.Converters;
using Hiper.Domain.DTO.Client;
using Hiper.Domain.Entities;
using Hiper.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hiper.Business
{
    public class ClientBusiness : IClientBusiness
    {
        private static string _currentDirectory = Directory.GetCurrentDirectory();
        private readonly IClientRepository _clientRepository;
        private readonly IRabbitMQSender _rabbitMQSender;
        private readonly IRabbitMQConsumer _rabbitMQConsumer;

        public ClientBusiness(IClientRepository clientRepository, IRabbitMQSender rabbitMQSender, IRabbitMQConsumer rabbitMQConsumer)
        {
            _rabbitMQSender = rabbitMQSender;
            _rabbitMQConsumer = rabbitMQConsumer;
            _clientRepository = clientRepository;
        }

        public async Task<ClientDTO> Add(ClientDTO ClientDTO)
        {
            var inc = ClientConverters.ConvertDtoToModel(ClientDTO);
            return ClientConverters.ConvertModelToDto(await _clientRepository.AddAsync(inc));
        }

        public async Task<ClientDTO> Alter(ClientDTO ClientDTO)
        {
            var alt = ClientConverters.ConvertDtoToModel(ClientDTO);
            return ClientConverters.ConvertModelToDto(await _clientRepository.AlterAsync(alt));
        }

        public async Task<bool> ConsumeClient()
        {
            try
            {
                await _rabbitMQConsumer.ConsumeClientQueue();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<ClientDTO>> GenerateExcel(List<ClientDTO> clientsDTO)
        {
            return await CreateExcel(clientsDTO);
        }

        private async Task<List<ClientDTO>> CreateExcel(List<ClientDTO> clients)
        {
            try
            {
                var workbook = new XLWorkbook();
                workbook.AddWorksheet("Clientes");
                var ws = workbook.Worksheet("Clientes");

                ws.Cell("A" + 1).Value = "ID";
                ws.Cell("B" + 1).Value = "NOME";
                ws.Cell("C" + 1).Value = "NASCIMENTO";
                ws.Cell("D" + 1).Value = "SEXO";
                ws.Cell("E" + 1).Value = "CPF";
                ws.Cell("F" + 1).Value = "EMAIL";
                ws.Cell("G" + 1).Value = "RUA";
                ws.Cell("H" + 1).Value = "NUMERO";
                ws.Cell("I" + 1).Value = "BAIRRO";
                ws.Cell("J" + 1).Value = "CIDADE";
                ws.Cell("K" + 1).Value = "ESTADO";
                ws.Cell("L" + 1).Value = "CEP";

                int row = 2;
                foreach (var c in clients)
                {
                    ws.Cell("A" + row.ToString()).Value = c.Id;
                    ws.Cell("B" + row.ToString()).Value = $"{c.FirstName} {c.LastName}";
                    ws.Cell("C" + row.ToString()).Value = c.Birthday.Date;
                    ws.Cell("D" + row.ToString()).Value = c.Gender == 0 ? "FEMININO" : "MASCULINO";
                    ws.Cell("E" + row.ToString()).Value = c.Document;
                    ws.Cell("F" + row.ToString()).Value = c.Email;
                    ws.Cell("G" + row.ToString()).Value = c.Street;
                    ws.Cell("H" + row.ToString()).Value = c.Number;
                    ws.Cell("I" + row.ToString()).Value = c.Neighborhood;
                    ws.Cell("J" + row.ToString()).Value = c.City;
                    ws.Cell("K" + row.ToString()).Value = c.State;
                    ws.Cell("L" + row.ToString()).Value = c.ZipCode;
                    row++;

                }

                workbook.SaveAs($"{_currentDirectory}\\Clientes.xlsx");

                FileInfo file = new FileInfo($"{_currentDirectory}\\Clientes.xlsx");

                if (file.Exists)
                {
                    var p = new System.Diagnostics.Process();
                    p.StartInfo = new System.Diagnostics.ProcessStartInfo($"{_currentDirectory}\\Clientes.xlsx")
                    {
                        UseShellExecute = true
                    };
                    p.Start();
                }

                return clients;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public async Task<List<ClientDTO>> GetAll(ClientFilterDTO filters)
        {
            var ret = await _clientRepository.GetAll(filters);
            List<ClientDTO> listRet = null;
            if (ret != null && ret.Any())
            {
                listRet = new List<ClientDTO>();
                foreach (var item in ret)
                    listRet.Add(ClientConverters.ConvertModelToDto(item));
            }
            return listRet;
        }

        public async Task<ClientDTO> GetbyDoc(string doc)
        {
            return ClientConverters.ConvertModelToDto(await _clientRepository.GetByDocument(doc));
        }

        public async Task<ClientDTO> GetbyId(int id)
        {
            return ClientConverters.ConvertModelToDto(await _clientRepository.GetById(id));
        }

        public async Task<bool> ImportClient(string file)
        {
            try
            {
                var listClient = await ReadCSV<ClientDTO>(file);

                _rabbitMQSender.SentClientQueue(listClient, "clientesQueue");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<List<TRetorno>> ReadCSV<TRetorno>(string path) where TRetorno : class
        {
            try
            {
                /*byte[] dadosAsBytes = Convert.FromBase64String(path);
                string resultado = ASCIIEncoding.ASCII.GetString(dadosAsBytes);*/

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ";",
                    Encoding = Encoding.UTF8,
                };

                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, config))
                    return csv.GetRecords<TRetorno>().ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClientDTO> Remove(ClientDTO ClientDTO)
        {
            var rem = ClientConverters.ConvertDtoToModel(ClientDTO);
            return ClientConverters.ConvertModelToDto(await _clientRepository.RemoveAsync(rem));
        }
    }
}
