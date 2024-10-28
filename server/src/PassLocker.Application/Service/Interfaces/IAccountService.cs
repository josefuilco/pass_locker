using PassLocker.Application.DTOs;
using PassLocker.Application.Generator.Interfaces;
using PassLocker.Domain.Entity;

namespace PassLocker.Application.Service.Interfaces;

public interface IAccountService
{
	Task CreateAccountAsync(Account account);
	Task CreateAccountWithoutPasswordAsync(Account account, IPasswordGenerator generator);
	Task<IEnumerable<AccountDTO>> GetAccountsByCategoryAsync(int categoryId);
	Task UpdateAccountAsync(Account account);
	Task DeleteAccountAsync(int accountId);
}