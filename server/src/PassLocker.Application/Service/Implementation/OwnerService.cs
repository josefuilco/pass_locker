using PassLocker.Application.Service.Interfaces;
using PassLocker.Domain.Repository;
using PassLocker.Domain.Service;

namespace PassLocker.Application.Service.Implementation;

public class OwnerService : IOwnerService
{
	private readonly IOwnerRepository repository;
	private readonly IEncryptService encryptService;

	public OwnerService(IOwnerRepository repository, IEncryptService encryptService)
	{
		this.repository = repository;
		this.encryptService = encryptService;
	}

	public async Task<bool> AuthenticateOwnerAsync(string masterPassword)
	{
		var owner = await repository.FindOwnerAsync();
		return masterPassword == encryptService.Decode(owner.MasterPassword!);
	}

	public async Task SetupMasterPasswordAsync(string masterPassword)
	{
		var cipheredMasterPassword = encryptService.Encode(masterPassword);
		await repository.UpdateOwnerAsync(cipheredMasterPassword);
	}
}