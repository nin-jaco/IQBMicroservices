using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace IQB.Rabbit.Factory
{
    public interface IRabbitMqConnectionFactory
    {
        IConnection CreateConnection();
    }
}
