using Hiper.Domain.DTO.Client;
using Hiper.View.HttpClients;
using Hiper.View.ServiceFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hiper.View.Gateway
{
    public class ClientGateway
    {
        private readonly ClientFactory _ClientFactory = new ClientFactory();

        public async Task<List<ClientDTO>> GetAll(ClientFilterDTO filtros)
        {
            var route = _ClientFactory.GetAll();
            var ReturnAPI = await HttpPatternAPI.Current.MakeAPIRequest<ClientFilterDTO, List<ClientDTO>>(route, filtros, HttpMethod.Post);
            if (ReturnAPI == null || !ReturnAPI.returnSucessRequisition)
                throw new Exception(ReturnAPI.ErrorMessage);

            return ReturnAPI.returnRequisitionAPI;
        }

        public async Task<ClientDTO> GetById(int id)
        {
            var ReturnAPI = await HttpPatternAPI.Current.MakeGetAPIRequest<ClientDTO>(_ClientFactory.GetById(id));

            if (ReturnAPI == null || !ReturnAPI.returnSucessRequisition)
                throw new Exception(ReturnAPI.ErrorMessage);

            return ReturnAPI.returnRequisitionAPI;
        }

        public async Task<bool> ComsumirClientes()
        {
            var ReturnAPI = await HttpPatternAPI.Current.MakeGetAPIRequest<string>(_ClientFactory.ConsumeClients());


            if (ReturnAPI == null || !ReturnAPI.returnSucessRequisition)
                throw new Exception(ReturnAPI.ErrorMessage);

            return true;
        }

        public async Task<bool> ImportarClientes(string arquivo)
        {
            var ReturnAPI = await HttpPatternAPI.Current.MakeAPIRequest<string, string>(_ClientFactory.ImportClients(), arquivo, HttpMethod.Post);


            if (ReturnAPI == null || !ReturnAPI.returnSucessRequisition)
                throw new Exception(ReturnAPI.ErrorMessage);

            return true;
        }

        public async Task<ClientDTO> Add(ClientDTO Cliente)
        {
            var ReturnAPI = await HttpPatternAPI.Current.MakeAPIRequest<ClientDTO, ClientDTO>(_ClientFactory.Add(), Cliente, HttpMethod.Post);

            if (ReturnAPI == null || !ReturnAPI.returnSucessRequisition)
                throw new Exception(ReturnAPI.ErrorMessage);

            return ReturnAPI.returnRequisitionAPI;
        }
        public async Task<List<ClientDTO>> GenerateExcel(List<ClientDTO> Clientes)
        {
            var ReturnAPI = await HttpPatternAPI.Current.MakeAPIRequest<List<ClientDTO>, List<ClientDTO>>(_ClientFactory.GenerateExcel(), Clientes, HttpMethod.Post);

            if (ReturnAPI == null || !ReturnAPI.returnSucessRequisition)
                throw new Exception(ReturnAPI.ErrorMessage);

            return ReturnAPI.returnRequisitionAPI;
        }
        public async Task<ClientDTO> Alter(ClientDTO Cliente)
        {
            var ReturnAPI = await HttpPatternAPI.Current.MakeAPIRequest<ClientDTO, ClientDTO>(_ClientFactory.Alter(), Cliente, HttpMethod.Put);

            if (ReturnAPI == null || !ReturnAPI.returnSucessRequisition)
                throw new Exception(ReturnAPI.ErrorMessage);

            return ReturnAPI.returnRequisitionAPI;
        }
        public async Task<ClientDTO> Remove(ClientDTO Cliente)
        {
            var ReturnAPI = await HttpPatternAPI.Current.MakeAPIRequest<ClientDTO, ClientDTO>(_ClientFactory.Remove(), Cliente, HttpMethod.Delete);

            if (ReturnAPI == null || !ReturnAPI.returnSucessRequisition)
                throw new Exception(ReturnAPI.ErrorMessage);

            return ReturnAPI.returnRequisitionAPI;
        }
    }
}
