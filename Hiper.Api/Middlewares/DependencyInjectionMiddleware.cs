using Hiper.Business;
using Hiper.Business.RabbitMQ.RabbitMQConsumer;
using Hiper.Business.RabbitMQ.RabbitMQSend;
using Hiper.Repository;
using Hiper.Repository.Interfaces;
using Hiper.Services;
using Hiper.Services.Interfaces;

namespace Hiper.Api.Middlewares
{
    public static class DependencyInjectionMiddleware
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configurações

            services.AddSingleton(configuration);

            #endregion

            #region Middlewares

            services.AddCors();

            #endregion

            #region Cliente

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientBusiness, ClientBusiness>();
            services.AddScoped<IClientServices, ClientServices>();

            #endregion

            #region RabbitMQ

            services.AddScoped<IRabbitMQConsumer, RabbitMQConsumer>();
            services.AddScoped<IRabbitMQSender, RabbitMQSender>();

            #endregion
        }
    }
}
