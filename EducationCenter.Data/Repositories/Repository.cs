﻿using EducationCenter.Data.DbContexts;
using EducationCenter.Data.IRepositories;
using EducationCenter.Domain.Commons;
using Microsoft.EntityFrameworkCore;

namespace EducationCenter.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly AppDbContext appDbContext;
    private readonly DbSet<TEntity> dbSet;

    public Repository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
        this.dbSet = appDbContext.Set<TEntity>();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await this.dbSet.FindAsync(id);
        this.dbSet.Remove(entity);
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var result = await this.dbSet.AddAsync(entity);

        return result.Entity;
    }
    public void UpdateAsync(TEntity entity)
    {
        this.dbSet.Update(entity);
    }

    public async Task<bool> SaveChangeAsync()
        => await this.appDbContext.SaveChangesAsync() > 0;

    public IQueryable<TEntity> GetAll()
        => dbSet;

    public async Task<TEntity> SelectByIdAsync(long id)
        => await this.dbSet.FindAsync(id);
}
