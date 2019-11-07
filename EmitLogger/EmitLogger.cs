using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace EmitLogger
{
	class EmitLogger
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory() {HostName = "localhost"};
			while (true)
			{
				using (var connection = factory.CreateConnection())
				using (var channel = connection.CreateModel())
				{
					channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

					var message = GetMessage();

					var body = Encoding.UTF8.GetBytes(message);
					channel.BasicPublish(exchange:"logs", routingKey:"", basicProperties:null, body:body);

					Console.WriteLine($"[x] Sent {message}");

					Thread.Sleep(3000);
				}
			}
		}

		private static string GetMessage()
		{
			return $"La La Land {DateTime.Now.ToLocalTime()}";
		}
	}
}
