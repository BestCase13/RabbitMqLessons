using System;
using System.Text;
using RabbitMQ.Client;

namespace Receiver
{
	class Receiver
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory() {HostName = "localhost"};
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "hello", durable:false, exclusive:false, autoDelete:false, arguments:null);
				string message = "Hello world";
				var body = Encoding.UTF8.GetBytes(message);

			}
		}
	}
}