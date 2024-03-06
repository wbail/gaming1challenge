namespace Gaming1Challenge.Application.Interfaces.Repositories.Data;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
}
