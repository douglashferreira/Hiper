using Hiper.Business;
using Hiper.Domain.DTO.Client;
using Hiper.Services.Interfaces;

namespace Hiper.Services
{
    public class ClientServices : IClientServices
    {
        private readonly IClientBusiness _clienteBusiness;

        public ClientServices(IClientBusiness clienteBusiness)
        {
            _clienteBusiness = clienteBusiness;
        }

        public async Task<ClientDTO> Add(ClientDTO clientDTO)
        {
            return await _clienteBusiness.Add(clientDTO);
        }

        public async Task<ClientDTO> Alter(ClientDTO clientDTO)
        {
            return await _clienteBusiness.Alter(clientDTO);
        }

        public async Task<bool> ConsumeClients()
        {
            return await _clienteBusiness.ConsumeClient();
        }

        public async Task<List<ClientDTO>> GenerateExcel(List<ClientDTO> clientsDTO)
        {
            return await _clienteBusiness.GenerateExcel(clientsDTO);
        }

        public async Task<List<ClientDTO>> GetAll(ClientFilterDTO filters)
        {
            return await _clienteBusiness.GetAll(filters);
        }

        public async Task<ClientDTO> GetByDocument(string document)
        {
            return await _clienteBusiness.GetbyDoc(document);
        }

        public async Task<ClientDTO> GetById(int id)
        {
                return await _clienteBusiness.GetbyId(id);
        }

        public async Task<bool> ImportClients(string file)
        {
            return await _clienteBusiness.ImportClient(file);
        }

        public async Task<ClientDTO> Remove(ClientDTO clientDTO)
        {
            return await _clienteBusiness.Remove(clientDTO);
        }
    }
}