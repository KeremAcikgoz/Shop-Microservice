using Contracts;
using MassTransit;
using Shop.Common;

namespace Shop.Order.Consumers
{
    public class CustomerUpdatedConsumer : IConsumer<CustomerUpdated>
    {
        private readonly IRepository<Shop.Order.Entities.Customer> _repository;

        public CustomerUpdatedConsumer(IRepository<Shop.Order.Entities.Customer> repository)
        {
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<CustomerUpdated> context)
        {
            var message = context.Message;

            var item = await _repository.GetAsync(message.Id);

            if(item==null)
            {
                item = new Entities.Customer
                {
                    Name = message.Name,
                    Email = message.Email,
                    Id = message.Id
                };

                await _repository.CreateAsync(item);
            }
            else
            {
                item.Name = message.Name;
                item.Email = message.Email;
                await _repository.UpdateAsync(item);
            }

        }

    }
}
