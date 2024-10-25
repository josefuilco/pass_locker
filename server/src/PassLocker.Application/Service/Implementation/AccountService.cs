using PassLocker.Application.DTOs;
using PassLocker.Application.Generator.Interfaces;
using PassLocker.Application.Service.Interfaces;
using PassLocker.Domain.Entity;
using PassLocker.Domain.Repository;
using PassLocker.Domain.Service;

namespace PassLocker.Application.Service.Implementation;

public class AccountService : IAccountService
{
	private readonly IAccountRepository repository;
	private readonly IEncryptService encryptService;

	public AccountService(IAccountRepository repository, IEncryptService encryptService)
	{
		this.repository = repository;
		this.encryptService = encryptService;
	}

	public async Task CreateAccountAsync(Account account)
	{
		if (account.Password == null) throw new Exception("Contraseña vacia");
		account.Password = encryptService.Encode(account.Password);
        await repository.SaveAccountAsync(account);
	}

	public async Task CreateAccountWithoutPassword(Account account, IPasswordGenerator generator)
    {
        account.Password = encryptService.Encode(generator.GeneratePassword());
		await repository.SaveAccountAsync(account);
    }

	public async Task<IEnumerable<AccountDTO>> GetAccountsByCategoryAsync(int categoryId)
	{
        var accounts = await repository.GetAccountsByCategoryAsync(categoryId);
		return from account
		in accounts
		select new AccountDTO(
			account.Id,
			account.Name ?? "Unknown",
			encryptService.Decode(account.Password!),
			account.Platform?.UrlImage ?? "Unknown",
			account.Platform?.Name ?? "Unknown",
			account.Tag?.Name ?? "Unknown"
		);
	}

	public async Task UpdateAccountAsync(Account account)
    {
		account.Password = encryptService.Encode(account.Password ?? "Unknown");
        await repository.UpdateAccountAsync(account);
    }

	public async Task DeleteAccountAsync(int accountId)
	{
        await repository.RemoveAccountAsync(accountId);
	}
}