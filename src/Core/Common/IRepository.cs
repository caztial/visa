namespace Core.Interfaces;

public interface IRepository<T>
    where T : class, IAggregatedRoot { }
