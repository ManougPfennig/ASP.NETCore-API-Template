using System.Reflection;
using ProjectTemplate.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectTemplate.Infrastructure;

public static class ServiceCollectionExtensions
{
	public static void AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<MailingListRepository>();
	}
}