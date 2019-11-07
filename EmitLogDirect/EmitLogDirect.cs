using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace EmitLogDirect
{
	class EmitLogDirect
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory() {HostName = "localhost"};
			while (true)
			{
				using (var connection = factory.CreateConnection())
				using (var channel = connection.CreateModel())
				{
					channel.ExchangeDeclare(exchange: "direct_logs",
						type: "direct");

					var rnd = new Random();
					var severity = rnd.Next(1, 2);

					var message = severity == 1
						? $"Error Message {DateTime.Now.ToLocalTime()}"
						: $"Info Message {DateTime.Now.ToLocalTime()}";

					var body = Encoding.UTF8.GetBytes(message);

					channel.BasicPublish(exchange: "direct_logs",
						routingKey: severity.ToString(),
						basicProperties: null,
						body: body);

					Console.WriteLine($" [x] Sent {message} with {severity}");

					Thread.Sleep(2000);
				}
			}
		}
	}
}