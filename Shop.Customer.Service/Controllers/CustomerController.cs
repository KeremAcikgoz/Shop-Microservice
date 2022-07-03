using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shop.Common;

namespace Shop.Customer.Service.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Entities.Customer> _customersRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CustomerController(IRepository<Entities.Customer> customersRepository, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _customersRepository = customersRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerDto>> GetAsync()
        {
            var customers = (await _customersRepository.GetAllAsync())
                .Select(customer => customer.AsDto());
            return customers; 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetByIdAsync(Guid id)
        {
            var customer = await _customersRepository.GetAsync(id);

            if (customer == null)
                return NotFound();
            
            return customer.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = new Entities.Customer
            {
                Name = createCustomerDto.Name,
                Address = createCustomerDto.Address,
                CreatedDate = DateTimeOffset.UtcNow,
                Email = createCustomerDto.Email
            };

            await _customersRepository.CreateAsync(customer);

            await _publishEndpoint.Publish(new CustomerCreated(customer.Id, customer.Name, customer.Email));

            return CreatedAtAction(nameof(GetByIdAsync), new { customer.Id }, customer);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateCustomerDto updateCustomerDto)
        {
            var existingCustomer = await _customersRepository.GetAsync(id);

            if (existingCustomer == null)
                return NotFound();

            existingCustomer.Name = updateCustomerDto.Name;
            existingCustomer.Address = updateCustomerDto.Address;
            existingCustomer.Email = updateCustomerDto.Email;

            await _customersRepository.UpdateAsync(existingCustomer);

            await _publishEndpoint.Publish(new CustomerUpdated(existingCustomer.Id, existingCustomer.Name, existingCustomer.Email));


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var customer = await _customersRepository.GetAsync(id);
            
            if (customer == null)
                return NotFound();
            
            await _customersRepository.RemoveAsync(customer.Id);

            await _publishEndpoint.Publish(new CustomerDeleted(customer.Id));

            return NoContent();
        }
    }
}