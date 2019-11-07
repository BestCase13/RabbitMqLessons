using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Worker
{
	class Worker
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "task_queue",
					durable: true, 
					exclusive: false, 
					autoDelete: false, 
					arguments: null);

				channel.BasicQos(prefetchSize:0, prefetchCount:1, global:false);

				Console.WriteLine("[x]Waiting for a messages");

				var consumer = new EventingBasicConsumer(channel);
				consumer.Received += (model, eventArgs) =>
				{
					var body = eventArgs.Body;
					var message = Encoding.UTF8.GetString(body);
					Console.WriteLine($"[x] Message Received {message}");

					var dots = message.Split('.').Length-1;
					Thread.Sleep(dots*1000);

					Console.WriteLine("[x] Done");

					channel.BasicAck(deliveryTag:eventArgs.DeliveryTag, multiple:false);
				};

				channel.BasicConsume(queue: "task_queue", autoAck:false, consumer: consumer);
				Console.WriteLine("Press [enter] to exit.");
				Console.ReadLine();
			}
		}

	}
}
