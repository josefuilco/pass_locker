using PassLocker.Domain.Entity;

namespace PassLocker.Domain.Repository;

public interface IPlatformRepository
{
	Task SavePlatformAsync(Platform platform);
	Task<IEnumerable<Platform>> GetPlatformsByCategoryAsync(int categoryId);
	Task UpdatePlatformAsync(Platform platform);
	Task RemovePlatformAsync(int platformId);
}