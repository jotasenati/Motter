using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Interfaces
{
    public interface IMessageProducer
    {
        Task PublishAsync<T>(string exchange, string routingKey, T message);
    }
}
