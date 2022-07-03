using System;
using Shop.Customer.Service.Model;
using Shop.Order.Model;

namespace Shop.Order
{
    public record OrderDto(Guid Id, Guid CustomerId, int Quantity, double Price, string Status, Address Address,
        Product Product, DateTimeOffset CreatedAt, DateTimeOffset UpdatedAt);

    public record CreateOrderDto(Guid Id, Guid CustomerId, int Quantity, double Price, string Status, Address Address,
        Product Product);

}