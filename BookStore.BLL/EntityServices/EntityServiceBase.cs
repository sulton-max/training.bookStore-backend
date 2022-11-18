using BookStore.BLL.EntityServices.Interfaces;
using BookStore.Core.Exceptions;
using BookStore.Core.Models.Common;
using BookStore.Core.Models.Entities;
using BookStore.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace BookStore.BLL.EntityServices;

/// <summary>
/// Provides common business logic for manipulating entites
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TRepository"></typeparam>
public class EntityServiceBase<TEntity, TRepository> : IEntityServiceBase<TEntity>
        where TEntity : class, IEntity
        where TRepository : IRepositoryBase<TEntity>
{
    protected readonly TRepository EntityRepository;

    public EntityServiceBase(TRepository entityRepository)
    {
        EntityRepository = entityRepository;
    }

    public virtual async Task<IEnumerable<TEntity>> Get(int pageSize, int pageToken)
    {
        if (pageSize <= 0 || pageToken <= 0)
            throw new ArgumentException();

        return await Task.Run(() => EntityRepository.Get(x => true).Skip((pageToken - 1) * pageSize).Take(pageSize).ToList());
    }

    public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
    {
        return EntityRepository.Get(expression);
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException();

        return await Task.Run(() => EntityRepository.Get(x => x.Id == id).FirstOrDefault());
    }

    public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> Ids)
    {
        if (Ids == null || Ids.Count() == 0)
            throw new ArgumentException();

        return await Task.Run(() => EntityRepository.Get(x => Ids.Contains(x.Id)).ToList());
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity, bool saveChanges = true)
    {
        if (entity == null)
            throw new ArgumentException();

        return await Task.Run(async () =>
        {
            EntityRepository.Create(entity);
            return saveChanges
                ? await EntityRepository.SaveChangesAsync() ? entity : throw new InvalidOperationException()
                : entity;
        });
    }

    public virtual async Task<bool> UpdateAsync(int id, TEntity entity, bool saveChanges = true)
    {
        if (id <= 0 || entity == null || id != entity.Id)
            throw new ArgumentException();

        return await Task.Run(async () =>
        {
            var foundEntity = await GetByIdAsync(id) ?? throw new EntryNotFoundException(nameof(TEntity));
            EntityRepository.Update(entity);
            return saveChanges
                ? await EntityRepository.SaveChangesAsync()
                : true;
        });
    }

    public virtual async Task<bool> DeleteByIdAsync(int id, bool saveChanges = true)
    {
        if (id <= 0)
            throw new ArgumentException($"Couldn't delete {nameof(TEntity)} due to invalid id");

        return await Task.Run(async () =>
        {
            var entity = EntityRepository.Get(x => x.Id == id).FirstOrDefault() ?? throw new EntryNotFoundException($"Couldn't find entity of type {nameof(TEntity)} with Id {id}");
            return await DeleteAsync(entity, saveChanges);
        });
    }

    public virtual async Task<bool> DeleteAsync(TEntity entity, bool saveChanges = true)
    {
        if (entity == null)
            throw new ArgumentException($"Couldn't delete {nameof(TEntity)} due to invalid model");

        return await Task.Run(async () =>
        {
            EntityRepository.Delete(entity);
            return saveChanges
                ? await EntityRepository.SaveChangesAsync()
                : true;
        });
    }
}