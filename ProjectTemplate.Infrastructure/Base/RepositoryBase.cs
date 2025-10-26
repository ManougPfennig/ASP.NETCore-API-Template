using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProjectTemplate.Domain.Base;
using ProjectTemplate.Infrastructure.Database;

namespace ProjectTemplate.Infrastructure.Base;

public class BaseRepository<TEntity, TContext>(TContext dbContext) : IRepositoryBase<TEntity>
    where TEntity : EntityBase
    where TContext : CoreDbContext
{
    protected readonly TContext _dbContext = dbContext;

	public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
	{
		try
		{
			return await _dbContext.Set<TEntity>()
				.AsNoTracking()
				.FirstOrDefaultAsync(predicate);
		}
		catch (Exception ex)
		{
			throw new Exception($"Unable to get entity: {ex.Message}", ex);
		}
	}

	public async Task<TEntity?> GetByIdAsync(int id)
	{
		try
		{
			return await _dbContext.Set<TEntity>().FindAsync(id);
		}
		catch (Exception ex)
		{
			throw new Exception($"Impossible de récupérer l'entité: {ex.Message}", ex);
		}
	}

    public async Task<List<TEntity>> GetAllAsync()
    {
        try
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Impossible de récupérer les entités: {ex.Message}", ex);
        }
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return await _dbContext.Set<TEntity>()
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Impossible de récupérer les entités: {ex.Message}", ex);
        }
    }

    // --- CREATE ---

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        try
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"Impossible d'ajouter l'entité: {ex.Message}", ex);
        }
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
			await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Impossible d'ajouter les entités: {ex.Message}", ex);
        }
    }

    // --- UPDATE ---

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"Impossible de mettre à jour l'entité: {ex.Message}", ex);
        }
    }

    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Impossible de mettre à jour les entités: {ex.Message}", ex);
        }
    }

    // --- DELETE ---

    public async Task DeleteAsync(TEntity entity)
    {
        try
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Impossible de supprimer l'entité: {ex.Message}", ex);
        }
    }

    public async Task DeleteByIdAsync(int id)
    {
        try
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                throw new Exception($"Aucune entité trouvée avec l'ID {id}");

            await DeleteAsync(entity);
        }
        catch (Exception ex)
        {
            throw new Exception($"Impossible de supprimer l'entité: {ex.Message}", ex);
        }
    }

    public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Impossible de supprimer les entités: {ex.Message}", ex);
        }
    }

    // --- UTILITAIRES ---

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return await _dbContext.Set<TEntity>().AnyAsync(predicate);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la vérification de l'existence: {ex.Message}", ex);
        }
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        try
        {
            if (predicate == null)
                return await _dbContext.Set<TEntity>().CountAsync();

            return await _dbContext.Set<TEntity>().CountAsync(predicate);
        }
        catch (Exception ex)
        {
            throw new Exception($"Impossible de compter les entités: {ex.Message}", ex);
        }
    }

    public async Task<List<TEntity>> GetPagedAsync(int pageNumber, int pageSize)
    {
        try
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Impossible de récupérer les entités paginées: {ex.Message}", ex);
        }
    }
}