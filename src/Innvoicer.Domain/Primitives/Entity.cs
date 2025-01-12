using System;

namespace Innvoicer.Domain.Primitives;

[Serializable]
public abstract class Entity<TId>
{
    public virtual TId Id { get; set; }
}