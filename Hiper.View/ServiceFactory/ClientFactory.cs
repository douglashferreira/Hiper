
namespace Hiper.View.ServiceFactory
{
    public class ClientFactory : ServiceFactoryAPI
    {
        public override void RenameController()
        {
            _ControllerName = "Client";
        }

        public string GetAll()
        {
            return $"{RouteAPI()}";
        }
        public string GetById(int id)
        {
            return $"{RouteAPI()}/id/id?{id}";
        }
        public string GetByDocument(string cpf)
        {
            return $"{RouteAPI()}/document/{cpf}";
        }
        public string Add()
        {
            return $"{RouteAPI()}/add";
        }
        public string GenerateExcel()
        {
            return $"{RouteAPI()}/Excel";
        }
        public string ConsumeClients()
        {
            return $"{RouteAPI()}/consumeClients";
        }
        public string ImportClients()
        {
            return $"{RouteAPI()}/importClients";
        }
        public string Alter()
        {
            return $"{RouteAPI()}/alter";
        }
        public string Remove()
        {
            return $"{RouteAPI()}/remove";
        }
    }
}
