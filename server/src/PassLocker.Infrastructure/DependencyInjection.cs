using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PassLocker.Domain.Repository;
using PassLocker.Domain.Service;
using PassLocker.Infrastructure.Connection;
using PassLocker.Infrastructure.Repository;
using PassLocker.Infrastructure.Service;

namespace PassLocker.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var mySqlConnection = configuration.GetConnectionString("MySqlConnection") ?? throw new Exception("MySQl connection not found");

		services.AddDbContextPool<PassLockerDbContext>(options =>
			options.UseMySQL(mySqlConnection));

		services.AddScoped<IEncryptService, AesEncryptService>();
		
		services.AddScoped<IAccountRepository, EfAccountRepository>();
		services.AddScoped<ICategoryRepository, EfCategoryRepository>();
		services.AddScoped<IOwnerRepository, EfOwnerRepository>();
		services.AddScoped<IPlatformRepository, EfPlatformRepository>();
		services.AddScoped<ITagRepository, EfTagRepository>();

		return services;
	}
}
