using PassLocker.Domain.Entity;

namespace PassLocker.Domain.Repository;

public interface IAccountRepository
{
	Task SaveAccountAsync(Account account);
	Task<IEnumerable<Account>> GetAccountsByCategoryAsync(int categoryId);
	Task UpdateAccountAsync(Account account);
	Task RemoveAccountAsync(int accountId);
}