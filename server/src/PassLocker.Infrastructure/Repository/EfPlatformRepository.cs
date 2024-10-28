using Microsoft.EntityFrameworkCore;
using PassLocker.Domain.Entity;
using PassLocker.Domain.Repository;
using PassLocker.Infrastructure.Connection;

namespace PassLocker.Infrastructure.Repository;

public class EfPlatformRepository : IPlatformRepository
{
	private readonly PassLockerDbContext dbContext;

	public EfPlatformRepository(PassLockerDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

    public async Task<IEnumerable<Platform>> GetPlatformsByCategoryAsync(int categoryId)
    {
        var platforms = await dbContext.Platform!.Where(p => p.Category!.Id == categoryId).ToListAsync()
			?? throw new Exception("Platforms by category not found");
		return platforms;
    }

    public async Task RemovePlatformAsync(int platformId)
    {
        var platform = await dbContext.Platform!.Where(p => p.Id == platformId).FirstOrDefaultAsync()
			?? throw new Exception("Platform not found");
		platform.IsActive = false;
		await dbContext.SaveChangesAsync();
    }

    public async Task SavePlatformAsync(Platform platform)
    {
        await dbContext.Platform!.AddAsync(platform);
		await dbContext.SaveChangesAsync();
    }

    public async Task UpdatePlatformAsync(Platform platform)
    {
        var currentPlatform = await dbContext.Platform!.Where(p => p.Id == platform.Id).FirstOrDefaultAsync()
			?? throw new Exception("Platform not found");
		currentPlatform.Name = platform.Name;
		currentPlatform.UrlImage = platform.UrlImage;
		currentPlatform.CategoryId = platform.CategoryId;
		await dbContext.SaveChangesAsync();
    }
}