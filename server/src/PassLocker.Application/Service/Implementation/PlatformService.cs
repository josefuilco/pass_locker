using PassLocker.Application.DTOs;
using PassLocker.Application.Service.Interfaces;
using PassLocker.Domain.Entity;
using PassLocker.Domain.Repository;

namespace PassLocker.Application.Service.Implementation;

public class PlatformService : IPlatformService
{
	private readonly IPlatformRepository repository;

	public PlatformService(IPlatformRepository repository)
	{
		this.repository = repository;
	}

	public async Task DeletePlatformAsync(int platformId)
	{
		await repository.RemovePlatformAsync(platformId);
	}

	public async Task<IEnumerable<PlatformDTO>> GetPlatformsByCategoryAsync(int categoryId)
	{
		var platforms = await repository.GetPlatformsByCategoryAsync(categoryId);
		return platforms.Select(p => new PlatformDTO(p.Id, p.Name ?? "Unknown"));
	}

	public async Task SavePlatformAsync(Platform platform)
	{
		await repository.SavePlatformAsync(platform);
	}

	public async Task UpdatePlatformAsync(Platform platform)
	{
		await repository.UpdatePlatformAsync(platform);
	}
}