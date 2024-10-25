using Microsoft.EntityFrameworkCore;
using PassLocker.Domain.Entity;
using PassLocker.Domain.Repository;
using PassLocker.Infrastructure.Connection;

namespace PassLocker.Infrastructure.Repository;

public class EfAccountRepository : IAccountRepository
{
	private readonly PassLockerDbContext dbContext;

	public EfAccountRepository(PassLockerDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task<IEnumerable<Account>> GetAccountsByCategoryAsync(int categoryId)
	{
		var accounts = await dbContext.Account!.Where(a => a.Id == categoryId).ToListAsync()
			?? throw new Exception("Accounts by category not found");
		return accounts;
	}

	public async Task RemoveAccountAsync(int accountId)
	{
		dbContext.Account!.Remove(new Account { Id = accountId });
		await dbContext.SaveChangesAsync();
	}

	public async Task SaveAccountAsync(Account account)
	{
		await dbContext.Account!.AddAsync(account);
		await dbContext.SaveChangesAsync();
	}

	public async Task UpdateAccountAsync(Account account)
	{
		var currentAccount = await dbContext.Account!.Where(a => a.Id == account.Id).FirstOrDefaultAsync()
			?? throw new Exception("Account not found for update");
		currentAccount.Name = account.Name;
		currentAccount.Password = account.Password;
		currentAccount.Platform!.Id = account.Platform!.Id;
		currentAccount.Tag!.Id = account.Tag!.Id;
		await dbContext.SaveChangesAsync();
	}
}