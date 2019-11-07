using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace ReceiveLogDirect
{
	class ReceiveLogDirect
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory() {HostName = "localhost"};

			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.ExchangeDeclare(exchange: "direct_logs", type: "direct");
				var queueName = channel.QueueDeclare().QueueName;
			}
		}

		private void DisplayError(string message)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(message);
			Console.ResetColor();
		}
		private void DisplayInfo(string message)
		{
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine(message);
			Console.ResetColor();
		}
	}
}