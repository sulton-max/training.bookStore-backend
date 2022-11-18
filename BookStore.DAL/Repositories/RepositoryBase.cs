using BookStore.Core.Models.Common;
using BookStore.DAL.Contexts;
using BookStore.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.DAL.Repositories;

/// <summary>
/// Provides common methods to manage entities
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> 
    where TEntity : class, IEntity
{
    public ApplicationDbContext DbContext { get; private set; }

    public RepositoryBase(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
    {
        return DbContext.Set<TEntity>().Where(expression);
    }

    public void Create(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }

    public bool SaveChanges()
    {
        return DbContext.SaveChanges() > 0;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await Task.Run(async () => await DbContext.SaveChangesAsync() > 0);
    }

    public void Detach(TEntity entity)
    {
        DbContext.Entry(entity).State = EntityState.Detached;
    }
}
