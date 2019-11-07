using System;
using System.Text;
using RabbitMQ.Client;

namespace Receiver
{
	class Sender
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory() {HostName = "localhost"};
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "lalaland",
					durable: false,
					exclusive: false,
					autoDelete: false,
					arguments: null);
				var message = "Hachapuri";
				var body = Encoding.UTF8.GetBytes(message);

				channel.BasicPublish(exchange: "",
					routingKey: "lalaland", 
					basicProperties: null,
					body: body);
				Console.WriteLine($"[x] Sent{message}");
			}

			Console.WriteLine(" Press [Enter] to exit");
			Console.ReadLine();
		}
	}
}