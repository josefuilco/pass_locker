using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PassLocker.Application.Service.Implementation;
using PassLocker.Application.Service.Interfaces;

namespace PassLocker.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		var assembly = typeof(DependencyInjection).Assembly;

		services.AddMediatR(configuration =>
			configuration.RegisterServicesFromAssembly(assembly));
			
		services.AddValidatorsFromAssembly(assembly);

		services.AddScoped<IAccountService, AccountService>();
		services.AddScoped<ICategoryService, CategoryService>();
		services.AddScoped<IOwnerService, OwnerService>();
		services.AddScoped<IPlatformService, PlatformService>();
		services.AddScoped<ITagService, TagService>();

		return services;
	}
}