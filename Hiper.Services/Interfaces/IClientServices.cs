using Hiper.Domain.DTO.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiper.Services.Interfaces
{
    public interface IClientServices
    {
        Task<List<ClientDTO>> GetAll(ClientFilterDTO filters);
        Task<ClientDTO> GetById(int id);
        Task<ClientDTO> GetByDocument(string document);
        Task<ClientDTO> Add(ClientDTO clienteDTO);
        Task<List<ClientDTO>> GenerateExcel(List<ClientDTO> clientesDTO);
        Task<bool> ImportClients(string file);
        Task<bool> ConsumeClients();
        Task<ClientDTO> Alter(ClientDTO clienteDTO);
        Task<ClientDTO> Remove(ClientDTO clienteDTO);
    }
}
