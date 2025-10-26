using Microsoft.Extensions.DependencyInjection;
using ProjectTemplate.Applications.Services;

namespace ProjectTemplate.Applications;

public static class ServiceCollectionExtensions
{
	public static void AddServices(this IServiceCollection services)
	{
		services.AddScoped<MailingListService>();
	}
}