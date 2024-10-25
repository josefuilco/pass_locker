namespace PassLocker.Application.Service.Interfaces;

public interface IOwnerService
{
	Task SetupMasterPasswordAsync(string masterPassword);
	Task<bool> AuthenticateOwnerAsync(string masterPassword);
}