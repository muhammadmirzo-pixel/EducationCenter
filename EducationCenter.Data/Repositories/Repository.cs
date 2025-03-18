using EducationCenter.Data.IRepositories;
using EducationCenter.Domain.Commons;

namespace EducationCenter.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    public Task DeleteAsync(long id)
    {
        return Task.CompletedTask;
    }

    public Task<IEnumerable<TEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> InsertAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveChangeAsync()
    {
        throw new NotImplementedException();
    }

    public Task<long> SelectByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public void UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
