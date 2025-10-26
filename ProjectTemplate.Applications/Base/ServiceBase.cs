using System.Linq.Expressions;
using ProjectTemplate.Infrastructure.Base;
using ProjectTemplate.Domain.Base;

namespace ProjectTemplate.Applications.Base;

public class ServiceBase<TRepository, TEntity>(TRepository repository) : IServiceBase<TEntity>
		where TRepository : IRepositoryBase<TEntity>
		where TEntity : EntityBase
{
	protected readonly TRepository _repository = repository;

	public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
	{
		return await _repository.FirstOrDefaultAsync(predicate);
	}

	public async Task<TEntity?> GetByIdAsync(int id)
	{
		return await _repository.GetByIdAsync(id);
	}

	public async Task<List<TEntity>> GetAllAsync()
	{
		return await _repository.GetAllAsync();
	}

	public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
	{
		return await _repository.GetAllAsync(predicate);
	}

	public async Task AddRangeAsync(IEnumerable<TEntity> entities)
	{
		await _repository.AddRangeAsync(entities);
	}

	public async Task<TEntity> AddAsync(TEntity entity)
	{
		return await _repository.AddAsync(entity);
	}

	public async Task<TEntity> UpdateAsync(TEntity entity)
	{
		return await _repository.UpdateAsync(entity);
	}

	public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
	{
		await _repository.UpdateRangeAsync(entities);
	}

	public async Task DeleteAsync(TEntity entity)
	{
		await _repository.DeleteAsync(entity);
	}

	public async Task DeleteByIdAsync(int id)
	{
		await _repository.DeleteByIdAsync(id);
	}

	public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
	{
		await _repository.DeleteRangeAsync(entities);
	}

	public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
	{
		return await _repository.ExistsAsync(predicate);
	}

	public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
	{
		return await _repository.CountAsync(predicate);
	}
	
	public async Task<List<TEntity>> GetPagedAsync(int pageNumber, int pageSize)
	{
		return await _repository.GetPagedAsync(pageNumber, pageSize);
	}
}