using Contracts;
using MassTransit;
using Shop.Common;

namespace Shop.Order.Consumers
{
    public class CustomerCreatedConsumer : IConsumer<CustomerCreated>
    {
        private readonly IRepository<Shop.Order.Entities.Customer> _repository;

        public CustomerCreatedConsumer(IRepository<Shop.Order.Entities.Customer> repository)
        {
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<CustomerCreated> context)
        {
            var message = context.Message;

            var item = await _repository.GetAsync(message.Id);

            if(item!=null)
            {
                return;
            }

            item = new Entities.Customer
            {
                Name = message.Name,
                Email = message.Email,
                Id = message.Id
            };

            await _repository.CreateAsync(item);
        }
    }
}
