using System;
using Core.Interfaces;

namespace Core.Common;

public abstract class BaseEntity
{
    public Guid Id { get; init; }
    public ICollection<IDomainEvent> Events { get; } = [];
}
