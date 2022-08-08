using Hiper.Domain.DTO.Client;
using Hiper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiper.Business
{
    public interface IClientBusiness
    {
        Task<List<ClientDTO>> GetAll(ClientFilterDTO filtros);
        Task<ClientDTO> GetbyId(int id);
        Task<ClientDTO> GetbyDoc(string cpf);
        Task<ClientDTO> Add(ClientDTO ClientDTO);
        Task<List<ClientDTO>> GenerateExcel(List<ClientDTO> clientsDTO);
        Task<bool> ImportClient(string arquivo);
        Task<bool> ConsumeClient();
        Task<ClientDTO> Alter(ClientDTO ClientDTO);
        Task<ClientDTO> Remove(ClientDTO ClientDTO);
    }
}
