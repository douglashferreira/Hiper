using Hiper.Domain.DTO.Client;
using Hiper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiper.Repository.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAll(ClientFilterDTO filters);
        Task<Client> GetById(int id);
        Task<Client> GetByDocument(string document);
        Task<Client> AddAsync(Client client);
        Task<Client> AlterAsync(Client client);
        Task<Client> RemoveAsync(Client client);
    }
}
