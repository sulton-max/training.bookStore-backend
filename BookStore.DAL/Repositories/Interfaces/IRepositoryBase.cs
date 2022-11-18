using BookStore.Core.Models.Common;
using BookStore.DAL.Contexts;
using System.Linq.Expressions;

namespace BookStore.DAL.Repositories.Interfaces;

/// <summary>
/// Defines common methods to manage entities
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepositoryBase<TEntity>
       where TEntity : class, IEntity
{
    /// <summary>
    /// Database context to access to Database
    /// </summary>
    ApplicationDbContext DbContext { get; }

    /// <summary>
    /// Gets entities matching the predicate
    /// </summary>
    /// <param name="expression">Predicate to get entities</param>
    /// <returns>Entities query</returns>
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Creates entry on Context scope
    /// </summary>
    /// <param name="entity">Entity to create</param>
    void Create(TEntity entity);

    /// <summary>
    /// Updates entity on context scope
    /// </summary>
    /// <param name="entity">Updated entity</param>
    void Update(TEntity entity);
    
    /// <summary>
    /// Deletes entity on context scope
    /// </summary>
    /// <param name="entity">Entity to delete</param>
    void Delete(TEntity entity);

    /// <summary>
    /// Sends context changes to the Database
    /// </summary>
    /// <returns>Result of whether any columns affected, true - affected columns count is greater than 0</returns>
    bool SaveChanges();

    /// <summary>
    /// Sends context changes to the Database
    /// </summary>
    /// <returns>Result of whether any columns affected, true - affected columns count is greater than 0</returns>
    Task<bool> SaveChangesAsync();

    /// <summary>
    /// Disables entity change tracking
    /// </summary>
    /// <param name="entity">Entity to detach</param>
    void Detach(TEntity entity);
}