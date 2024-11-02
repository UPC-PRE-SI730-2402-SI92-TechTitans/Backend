namespace Domain.Shared;

public interface IUnitOfWork
{
    Task CompleteAsync();
}