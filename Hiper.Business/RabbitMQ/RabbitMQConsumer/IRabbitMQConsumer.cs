using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiper.Business.RabbitMQ.RabbitMQConsumer
{
    public interface IRabbitMQConsumer
    {
        Task ConsumeClientQueue();
    }
}
