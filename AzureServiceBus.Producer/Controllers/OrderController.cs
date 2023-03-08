using AzureServiceBus.Producer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBusApp.Common;
using ServiceBusApp.Common.Dto;
using ServiceBusApp.Common.Events;

namespace AzureServiceBus.Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly AzureService _azureService;
        public OrderController(AzureService azureService)
        {
            _azureService = azureService;
        }

        [HttpPost]
        public async Task CreateOrder(OrderDto order)
        {
            //insert order into db sonrasında yeni bir order create edildi haberini bu queue yu dinleyenlere haber olarak gönderilicek
            var orderCreatedEvent = new OrderCreatedEvent()
            {
                Id = order.Id,
                ProductName = order.ProductName,
                CreatedOn = DateTime.Now
            };
            await _azureService.CreateQueueIfNotExists(Constants.OrderCreatedQueue);
            await _azureService.SendMessageToQueue(Constants.OrderCreatedQueue,orderCreatedEvent);
        }

        [HttpDelete("{id}")]
        public async Task DeleteOrder(int Id)
        {
            //insert order into db sonrasında yeni bir order create edildi haberini bu queue yu dinleyenlere haber olarak gönderilicek
            var orderDeletedEvent = new OrderDeletedEvent()
            {
                Id = Id,
                CreatedOn = DateTime.Now
            };
            await _azureService.CreateQueueIfNotExists(Constants.OrderDeletedQueue);
            await _azureService.SendMessageToQueue(Constants.OrderDeletedQueue, orderDeletedEvent);
        }
    }
}
