using Azure.Messaging.ServiceBus;
using Bakery.Commons.Bakery.Commons.Domain.Model;
using Bakery.Commons.Bakery.Commons.Domain.Port;
using Bakery.Inventory.DomainApi.Model;
using Bakery.Inventory.DomainApi.Port;
using Bakery.Inventory.Persistence.Adapter.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bakery.Inventory.Domain
{
    public class ProcessQueue : IProcessQueue
    {
        private readonly IServiceBusHelper _serviceBusHelper;
        private readonly IConfiguration _configuration;

        public ProcessQueue(IServiceBusHelper serviceBusHelper, IConfiguration configuration)
        {
            _serviceBusHelper = serviceBusHelper;
            _configuration = configuration;
        }

        public async Task Initialize()
        {
            await _serviceBusHelper.ProcessAsync(MessageHandler);
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            var message = JsonSerializer.Deserialize<ServiceBusMessage<InventorySold>>(body);

            ApplicationDbContext context = CreateTemporaryContext();
            if (message.Operation == ServiceBusOperation.Update)
            {
                var inventory = await context.Inventories.FirstOrDefaultAsync(x => x.ProductId == message.Message.ProductId);
                if (inventory != null)
                {
                    inventory.Quantity -= message.Message.Quantity;
                    inventory.User = message.User;
                    context.Update(inventory);
                    context.SaveChanges();
                }
            }

            await args.CompleteMessageAsync(args.Message);
        }

        private ApplicationDbContext CreateTemporaryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseSqlServer(_configuration.GetSection("SqlAzureInventory:ConnectionString").Value)
                            .Options;
            var context = new ApplicationDbContext(options);
            return context;
        }
    }
}
