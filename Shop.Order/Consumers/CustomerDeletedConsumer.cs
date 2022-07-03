using Contracts;
using MassTransit;
using Shop.Common;

namespace Shop.Order.Consumers
{
    public class CustomerDeletedConsumer : IConsumer<CustomerDeleted>
    {
        private readonly IRepository<Shop.Order.Entities.Customer> _repository;

        public CustomerDeletedConsumer(IRepository<Shop.Order.Entities.Customer> repository)
        {
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<CustomerDeleted> context)
        {
            var message = context.Message;

            var item = await _repository.GetAsync(message.Id);

            if(item==null)
            {
                return;
            }

            await _repository.RemoveAsync(message.Id);
        }
    }
}
