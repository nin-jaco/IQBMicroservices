using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace IQB.Rabbit.Factory
{
    public class RabbitMqConnection : IRabbitMqConnectionFactory
    {
        private readonly RabbitMqCredentials _connectionCredentials;

        public RabbitMqConnection(RabbitMqCredentials connectionCredentials)
        {
            _connectionCredentials = connectionCredentials;
        }

        public IConnection CreateConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = _connectionCredentials.HostName,
                UserName = _connectionCredentials.UserName,
                Password = _connectionCredentials.Password
            };
            var connection = factory.CreateConnection();
            return connection;
        }
    }
}
