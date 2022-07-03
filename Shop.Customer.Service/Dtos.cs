using System;
using System.ComponentModel.DataAnnotations;
using Shop.Customer.Service.Model;

namespace Shop.Customer.Service
{
    public record CustomerDto(Guid Id, string Name, string Email, Address Address, DateTimeOffset CreatedDate);

    public record CreateCustomerDto([Required] string Name, [Required] string Email, [Required] Address Address);

    public record UpdateCustomerDto([Required] string Name, [Required] string Email, [Required] Address Address);
}