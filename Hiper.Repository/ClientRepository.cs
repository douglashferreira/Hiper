using Hiper.Domain.DTO.Client;
using Hiper.Domain.Entities;
using Hiper.Repository.Interfaces;
using Hiper.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiper.Repository
{
    public class ClientRepository : IClientRepository
    {
        public async Task<Client> AddAsync(Client client)
        {
            try
            {
                HiperContext hiperContext = new HiperContext();
                hiperContext.Clients.Add(client);
                hiperContext.SaveChanges();
                return client;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<Client> AlterAsync(Client client)
        {
            HiperContext hiperContext = new HiperContext();
            var entity = hiperContext.Clients.FirstOrDefault(f => f.Id == client.Id);
            if (entity != null)
            {
                hiperContext.Entry(entity).CurrentValues.SetValues(client);
                hiperContext.Entry(entity.Address).CurrentValues.SetValues(client.Address);
                hiperContext.Entry(entity.Name).CurrentValues.SetValues(client.Name);
                hiperContext.Entry(entity.Document).CurrentValues.SetValues(client.Document);
                hiperContext.Entry(entity.Email).CurrentValues.SetValues(client.Email);
            }
            hiperContext.SaveChanges();
            return client;
        }

        public async Task<List<Client>> GetAll(ClientFilterDTO filters)
        {
            HiperContext hiperContext = new HiperContext();
            List<Client> list;
            if (filters.id > 0)
                list = hiperContext.Clients.Where(w => w.Id == filters.id).ToList();
            else if (!string.IsNullOrEmpty(filters.document))
                list = hiperContext.Clients.Where(w => w.Document.Number == filters.document).ToList();
            else
                list = hiperContext.Clients.Where(w => 1 == 1).ToList();
            
            return list;
        }

        public async Task<Client> GetByDocument(string document)
        {
            HiperContext hiperContext = new HiperContext();
            var ret = hiperContext.Clients.FirstOrDefault(f => f.Document.Number == document);
            return ret;
        }

        public async Task<Client> GetById(int id)
        {
            HiperContext hiperContext = new HiperContext();
            return hiperContext.Clients.FirstOrDefault(f => f.Id == id);
        }

        public async Task<Client> RemoveAsync(Client client)
        {
            HiperContext hiperContext = new HiperContext();
            var remove = hiperContext.Clients.FirstOrDefault(f => f.Id == client.Id);
            hiperContext.Clients.Remove(remove);
            hiperContext.SaveChanges();
            return client;
        }
    }
}
