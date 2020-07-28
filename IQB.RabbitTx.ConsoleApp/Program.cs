using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace IQB.RabbitTx.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Rabbit Transmitter Starting");
            var text = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Please enter a value.");
                Console.ReadLine();
            }
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "userQueue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(text);

                channel.BasicPublish(exchange: "",
                    routingKey: "userQueue",
                    basicProperties: null,
                    body: body);
            }
            Console.WriteLine("Name sent successfully");
            Console.ReadLine();
        }
    }
}
