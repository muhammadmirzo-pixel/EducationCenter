using EducationCenter.Domain.Commons;

namespace EducationCenter.Data.IRepositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    Task<bool> SaveChangeAsync ();
    void UpdateAsync (TEntity entity);
    Task DeleteAsync (long id);
    Task<TEntity> SelectByIdAsync (long id);
    IQueryable<TEntity> GetAll ();
    Task<TEntity> InsertAsync (TEntity entity);
}
