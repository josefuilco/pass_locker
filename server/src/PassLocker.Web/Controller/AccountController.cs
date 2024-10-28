using Microsoft.AspNetCore.Mvc;
using PassLocker.Application.Generator.Implementation;
using PassLocker.Application.Service.Interfaces;
using PassLocker.Domain.Entity;
using PassLocker.Web.Filter;
using PassLocker.Web.Request;
using PassLocker.Web.Response;

namespace PassLocker.Web.Controller;

[SessionVerifier]
[ApiController]
[Route("api/v1/accounts")]
public class AccountController : ControllerBase
{
	private readonly ILogger<AccountController> _logger;
	private readonly IAccountService _service;

	public AccountController(ILogger<AccountController> logger, IAccountService service)
	{
		_logger = logger;
		_service = service;
	}

	[HttpGet]
	[Route("{categoryId}")]
	public async Task<IActionResult> GetAllAccountsByCategory(int categoryId)
	{
		try
		{
			var accounts = await _service.GetAccountsByCategoryAsync(categoryId);
			_logger.LogInformation("Accounts found are: {accounts.Count()}", accounts.Count());
			return Ok(new ApiResponse("Accounts found", accounts));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}

	[HttpPost]
	public async Task<IActionResult> CreateAccount([FromBody] AccountRequest body)
	{
		try
		{
			await _service.CreateAccountAsync(
				new Account
				{
					Name = body.Name,
					Password = body.Password,
					PlatformId = body.PlatformId,
					TagId = body.TagId
				}
			);
			_logger.LogInformation("Account created: {body.Name}", body.Name);
			return Ok(new ApiResponse("Account created", true));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}

	[HttpPost]
	[Route("basic-generate")]
	public async Task<IActionResult> CreateAccountWithBasicPassword([FromBody] AccountGeneratedPasswordRequest body)
	{
		try
		{
			await _service.CreateAccountWithoutPasswordAsync(
				new Account
				{
					Name = body.Name,
					PlatformId = body.PlatformId ,
					TagId = body.TagId
				},
				new BasicPasswordGenerator()
			);
			_logger.LogInformation("Account created: {body.Name}", body.Name);
			return Ok(new ApiResponse("Account created", true));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}

	[HttpPost]
	[Route("advanced-generate/{length}")]
	public async Task<IActionResult> CreateAccountWithBasicPassword([FromBody] AccountGeneratedPasswordRequest body, int length)
	{
		try
		{
			await _service.CreateAccountWithoutPasswordAsync(
				new Account
				{
					Name = body.Name,
					PlatformId = body.PlatformId,
					TagId = body.TagId
				},
				new AdvancedPasswordGenerator(length)
			);
			_logger.LogInformation("Account created: {body.Name}", body.Name);
			return Ok(new ApiResponse("Account created", true));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}

	[HttpPut]
	[Route("{id}")]
	public async Task<IActionResult> UpdateAccount([FromBody] AccountRequest body, int id)
	{
		try
		{
			await _service.UpdateAccountAsync(
				new Account
				{
					Id = id,
					Name = body.Name,
					Password = body.Password,
					PlatformId = body.PlatformId,
					TagId = body.TagId
				}
			);
			_logger.LogInformation("Account updated: {id}", id);
			return Ok(new ApiResponse("Account updated", true));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}

	[HttpDelete]
	[Route("{id}")]
	public async Task<IActionResult> DeleteAccount(int id)
	{
		try
		{
			await _service.DeleteAccountAsync(id);
			_logger.LogInformation("Account deleted: {id}", id);
			return Ok(new ApiResponse("Account deleted", true));
		}
		catch (Exception ex)
		{
			_logger.LogError("Is {ex.Message}", ex.Message);
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}
}