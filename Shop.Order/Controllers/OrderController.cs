using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Common;

namespace Shop.Order.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Entities.Order> _orderRepository;
        private readonly IRepository<Entities.Customer> _customerRepository;

        public OrderController(IRepository<Entities.Order> orderRepository, IRepository<Entities.Customer> customerRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAsync(Guid CustomerId)
        {
            if (CustomerId == Guid.Empty)
            {
                return BadRequest();
            }

            //var orderEntities = await _orderRepository.GetAllAsync(item => item.CustomerId == CustomerId);
            //var Ids = orderEntities.Select(item => item.CustomerId);
            //var customerEntities = await _customerRepository.GetAllAsync(item => Ids.Contains(item.Id));

            //var orderDtos = orderEntities.Select(orderItem =>
            //{
            //    var customerItem = customerEntities.Single(customerItem => customerItem.Id == orderItem.CustomerId);
            //    return orderItem.AsDto(customerItem.Id,customerItem.Name, customerItem.Email);
            //});
            //todo bunu düzelt

            var orders = (await _orderRepository.GetAllAsync(order => order.CustomerId == CustomerId))
                .Select(order => order.AsDto());

            return Ok(orders);

        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(CreateOrderDto createOrderDto)
        {
            var orderItem = await _orderRepository.GetAsync(order =>
                order.CustomerId == createOrderDto.CustomerId && order.Id == createOrderDto.Id);

            if (orderItem == null)
            {
                orderItem = new Entities.Order()
                {
                    CustomerId = createOrderDto.CustomerId,
                    Id = createOrderDto.Id,
                    Address = createOrderDto.Address,
                    Quantity = createOrderDto.Quantity,
                    Price = createOrderDto.Price,
                    Status = createOrderDto.Status,
                    Product = createOrderDto.Product,
                    CreatedAt = DateTimeOffset.Now
                };

                await _orderRepository.CreateAsync(orderItem);
            }

            else
            {
                orderItem.Quantity += 1;
                await _orderRepository.UpdateAsync(orderItem);
            }

            return Ok();
        }
    }
}