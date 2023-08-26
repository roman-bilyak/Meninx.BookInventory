namespace Meninx.BookInventory
{
    public abstract class Entity
    {
    }

    public abstract class Entity<T>
        where T : struct
    {
        public T Id { get; set; } //TODO:revert set method to protected

        protected Entity()
        {

        }

        protected Entity(T id)
        {
            Id = id;
        }
    }
}
