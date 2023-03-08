using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.ServiceBus.Management;
using Newtonsoft.Json;
using ServiceBusApp.Common;
using System.Text;

namespace AzureServiceBus.Producer.Services
{
    public class AzureService
    {

        private readonly ManagementClient _managementClient;

        public AzureService(ManagementClient managementClient)
        {
            _managementClient = managementClient;
        }

        public async Task SendMessageToQueue(string queueName, object messageContent)
        {
            IQueueClient client = new QueueClient(Constants.ConnectionString,queueName);

            await SendMessage(client, messageContent);
        }

        public async Task CreateQueueIfNotExists(string queueName)
        {
            if(!await _managementClient.QueueExistsAsync(queueName))
            {
                await _managementClient.CreateQueueAsync(queueName);
            }

        }

        public async Task SendMessageToTopic(string topicName, object messageContent)
        {
            ITopicClient client = new TopicClient(Constants.ConnectionString, topicName);
            await SendMessage(client, messageContent);
        }


        public async Task CreateTopicIfNotExists(string topicName)
        {
            if (!await _managementClient.TopicExistsAsync(topicName))
                await _managementClient.CreateTopicAsync(topicName);
        }


        private async Task SendMessage(ISenderClient client, object messageContent)
        {
            var byteArr = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageContent));
            var message = new Message(byteArr);

            await client.SendAsync(message);
        }

    }
}
