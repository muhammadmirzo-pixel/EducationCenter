using EducationCenter.Domain.Commons;

namespace EducationCenter.Data.IRepositories;

public interface IRepository<TEntity>
{
    Task<TEntity> InsertAsync (TEntity entity);
    Task DeleteAsync (long id);
    void UpdateAsync (TEntity entity);
    Task<TEntity> SelectByIdAsync (long id);
    IQueryable<TEntity> GetAll ();
    Task<bool> SaveChangeAsync ();
}
