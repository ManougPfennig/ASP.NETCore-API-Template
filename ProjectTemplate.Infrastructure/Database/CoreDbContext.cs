using System.Reflection;
using ProjectTemplate.Domain.Base;
using ProjectTemplate.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectTemplate.Infrastructure.Database;

public class CoreDbContext(DbContextOptions<CoreDbContext> options)  : DbContext(options)
{
	public DbSet<MailingList> MailingList => Set<MailingList>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
	
	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var entries = ChangeTracker.Entries<EntityBase>();
		var now = DateTime.UtcNow;

		foreach (var entry in entries)
		{
			switch (entry.State)
			{
				case EntityState.Added:
					entry.Entity.SetCreationTime();
					break;

				case EntityState.Modified:
					entry.Entity.SetUpdateTime();
					break;
			}
		}

		return await base.SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
	}
}