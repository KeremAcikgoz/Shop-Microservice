using System;

namespace Contracts
{
    public record CustomerCreated(Guid Id, string Name, string Email);
    public record CustomerUpdated(Guid Id, string Name, string Email);
    public record CustomerDeleted(Guid Id);
}