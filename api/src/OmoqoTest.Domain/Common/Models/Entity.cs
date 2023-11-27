namespace OmoqoTest.Domain.Common.Models
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        public TId? Id { get; protected set; }

        public override bool Equals(object? obj) => obj is Entity<TId> entity && Id is not null && Id.Equals(entity.Id);

        public bool Equals(Entity<TId>? other) => Equals((object?)other);

        public static bool operator ==(Entity<TId> left, Entity<TId> right) => Equals(left, right);

        public static bool operator !=(Entity<TId> left, Entity<TId> right) => !Equals(left, right);

        public override int GetHashCode() => Id is not null ? Id.GetHashCode() : 0;

    }
}