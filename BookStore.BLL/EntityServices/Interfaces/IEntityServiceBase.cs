using BookStore.Core.Models.Common;
using BookStore.Core.Models.Entities;
using System.Linq.Expressions;

namespace BookStore.BLL.EntityServices.Interfaces;

/// <summary>
/// Defines common business logic to manipulate entities
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityServiceBase<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Gets entities matching the predicate
    /// </summary>
    /// <param name="pageSize">Number of current section of entities being queried</param>
    /// <param name="pageToken">Number of entities to query</param>
    /// <returns>Entities query</returns>
    Task<IEnumerable<TEntity>> Get(int pageSize, int pageToken);

    /// <summary>
    /// Gets an entity that has the required Id
    /// /// </summary>
    /// <param name="id">Id of entity being queried</param>
    /// <returns>Entity if found, or null</returns>
    Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Gets entites that matches given array of Ids
    /// /// </summary>
    /// <param name="Ids">Ids of entities being queried</param>
    /// <returns>Array of entities or empty array</returns>
    Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> Ids);

    /// <summary>
    /// Creates entry on Context scope or on Database
    /// </summary>
    /// <param name="entity">Entity to create</param>
    /// <param name="saveChanges">Determines whether to commit changes to Database</param>
    /// <returns>Result of entity creation</returns>
    Task<TEntity> CreateAsync(TEntity entity, bool saveChanges = true);

    /// <summary>
    /// Updates entity on context scope or on Database
    /// </summary>
    /// <param name="entity">Updated entity</param>
    /// <param name="saveChanges">Determines whether to commit changes to Database</param>
    /// <returns>Result of entity update</returns>
    Task<bool> UpdateAsync(int id, TEntity entity, bool saveChanges = true);

    /// <summary>
    /// Deletes entity on context scope or on Database
    /// </summary>
    /// <param name="id">Id of Entity to delete</param>
    /// <param name="saveChanges">Determines whether to commit changes to Database</param>
    /// <returns></returns>
    Task<bool> DeleteByIdAsync(int id, bool saveChanges = true);

    /// <summary>
    /// Deletes entity on context scope or on Database with its Id
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="entity">Entity to delete</param>
    /// <param name="saveChanges">Determines whether to commit changes to Database</param>
    /// <returns></returns>
    Task<bool> DeleteAsync(TEntity entity, bool saveChanges = true);
}