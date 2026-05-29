namespace SomnosSuite.Domain.SharedKernel
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; private init; }

        protected BaseEntity() { }

        protected BaseEntity(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Id cannot be empty.");

            Id = id;
        }
    }
}
