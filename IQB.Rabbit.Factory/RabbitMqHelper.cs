using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace IQB.Rabbit.Factory
{
    public class RabbitMqHelper
    {
        private static IModel _model;
        private static string _exchangeName;
        const string RoutingKey = "dummy-key.";

        public RabbitMqHelper(IModel model, string exchangeName)
        {
            _model = model;
            _exchangeName = exchangeName;
        }


        public void SetupQueue(string queueName)
        {
            _model.ExchangeDeclare(_exchangeName, ExchangeType.Topic);
            _model.QueueDeclare(queueName, true, false, false, null);
            _model.QueueBind(queueName, _exchangeName, RoutingKey);
        }

        public void PushMessageIntoQueue(byte[] message, string queue)
        {
            SetupQueue(queue);
            _model.BasicPublish(_exchangeName, RoutingKey, null, message);
        }

        public byte[] ReadMessageFromQueue(string queueName)
        {
            SetupQueue(queueName);
            var data = _model.BasicGet(queueName, false);
            var message = data.Body.ToArray();
            _model.BasicAck(data.DeliveryTag, false);
            return message;
        }
    }
}
