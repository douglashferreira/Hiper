using System;

namespace Hiper.View.ServiceFactory
{
    public abstract class ServiceFactoryAPI
    {
        public ServiceFactoryAPI()
        {
            RenameController();
        }

        protected string _ControllerName { get; set; }

        protected string _PatternRoute
        {
            get
            {
                string path = "api/";

                return path;
            }
        }

        public abstract void RenameController();

        public virtual string RouteAPI()
        {
            if (string.IsNullOrEmpty(_ControllerName))
                throw new ArgumentNullException("Nome do controller da API não informado.");

            return $"{_PatternRoute}{_ControllerName}";
        }
    }
}
