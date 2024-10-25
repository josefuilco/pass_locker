using PassLocker.Application.DTOs;
using PassLocker.Domain.Entity;

namespace PassLocker.Application.Service.Interfaces;

public interface IPlatformService
{
	Task SavePlatformAsync(Platform platform);
	Task<IEnumerable<PlatformDTO>> GetPlatformsByCategoryAsync(int categoryId);
	Task UpdatePlatformAsync(Platform platform);
	Task DeletePlatformAsync(int platformId);
}