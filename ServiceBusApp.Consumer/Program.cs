
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBusApp.Common;
using ServiceBusApp.Common.Events;
using System.Text;

namespace ServiceBusApp.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsumeQueue<OrderCreatedEvent>(Constants.OrderCreatedQueue, i =>
            {
                Console.WriteLine($"id:{i.Id}, Name: {i.ProductName}");
            }).Wait();


            ConsumeQueue<OrderDeletedEvent>(Constants.OrderDeletedQueue, i =>
            {
                Console.WriteLine($"id:{i.Id}");
            }).Wait();

            Console.ReadLine();
        }

        private static async Task ConsumeQueue<T>(string queueName, Action<T> receivedAction)
        {
            IQueueClient client = new QueueClient(Constants.ConnectionString,queueName);
            client.RegisterMessageHandler(async (message, cancelationtoken) => 
            {
                var model = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(message.Body));

                receivedAction(model);

                await Task.CompletedTask;

            }, new MessageHandlerOptions(i => Task.CompletedTask));

            Console.WriteLine($"{typeof(T).Name} is listening");
        }

    }
}