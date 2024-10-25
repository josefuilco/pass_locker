using PassLocker.Domain.Entity;

namespace PassLocker.Domain.Repository;

public interface IOwnerRepository
{
	Task UpdateOwnerAsync(string cipheredMasterPassword);
	Task<Owner> FindOwnerAsync(); 
}
