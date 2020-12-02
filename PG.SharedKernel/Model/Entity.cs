using System;
using System.ComponentModel.DataAnnotations;
using PG.SharedKernel.Custom;

namespace PG.SharedKernel.Model
{
    public abstract class Entity<TId>
    {
        [Key]
        public virtual TId Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Boolean Void { get; set; }
        public DateTime? VoidDate { get; set; }
        public Guid UserId { get; set; }

        protected Entity()
        {
            AssignId();
        }

        protected Entity(TId id)
        {
            Id = id;
        }

        public virtual void AssignId()
        {
            if (typeof(TId) == typeof(Guid));
            Id = (TId) (object) LiveGuid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Entity<TId>;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id.Equals(default(TId)) || other.Id.Equals(default(TId)))
                return false;

            return Id.Equals(other.Id);
        }

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}
