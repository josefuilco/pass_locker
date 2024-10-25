using Microsoft.EntityFrameworkCore;
using PassLocker.Domain.Entity;
using PassLocker.Domain.Repository;
using PassLocker.Infrastructure.Connection;

namespace PassLocker.Infrastructure.Repository;

public class EfOwnerRepository : IOwnerRepository
{
	private readonly PassLockerDbContext dbContext;

	public EfOwnerRepository(PassLockerDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

    public async Task<Owner> FindOwnerAsync()
    {
        var owner = await dbContext.Owner!.Where(o => o.Id == 1).FirstOrDefaultAsync() ?? throw new Exception("Owner not found");
        return owner;
    }

    public async Task UpdateOwnerAsync(string cipheredMasterPassword)
    {
        var owner = await dbContext.Owner!.Where(o => o.Id == 1).FirstOrDefaultAsync() ?? throw new Exception("Owner not found for update");
		owner.MasterPassword = cipheredMasterPassword;
		await dbContext.SaveChangesAsync();
    }
}