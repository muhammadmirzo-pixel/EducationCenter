using EducationCenter.Domain.Commons;

namespace EducationCenter.Data.IRepositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    Task<bool> SaveChangeAsync ();
    void UpdateAsync (TEntity entity);
    Task DeleteAsync (long id);
    Task<long> SelectByIdAsync (long id);
    Task<IEnumerable<TEntity>> GetAllAsync ();
    Task<TEntity> InsertAsync (TEntity entity);
}
