using System.Linq.Expressions;
using ProjectTemplate.Domain.Base;

namespace ProjectTemplate.Infrastructure.Base;

public interface IRepositoryBase<T> where T : EntityBase
{
	Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
	Task<T?> GetByIdAsync(int id);
	Task<List<T>> GetAllAsync();
	Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
	Task AddRangeAsync(IEnumerable<T> entities);
	Task<T> AddAsync(T entity);
	Task<T> UpdateAsync(T entity);
	Task UpdateRangeAsync(IEnumerable<T> entities);
	Task DeleteAsync(T entity);
	Task DeleteByIdAsync(int id);
	Task DeleteRangeAsync(IEnumerable<T> entities);
	Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
	Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
	Task<List<T>> GetPagedAsync(int pageNumber, int pageSize);
}
