using Microsoft.AspNetCore.Mvc;
using PassLocker.Application.Service.Interfaces;
using PassLocker.Web.Configuration;
using PassLocker.Web.Request;
using PassLocker.Web.Response;
using PassLocker.Web.Filter;

namespace PassLocker.Web.Controller;

[ApiController]
[Route("api/v1/auth")]
public class AuthenticationController : ControllerBase
{
	private readonly ILogger<AuthenticationController> _logger;
	private readonly CookieConfiguration _cookieConfig;
	private readonly IOwnerService _service;

	public AuthenticationController(
		ILogger<AuthenticationController> logger,
		CookieConfiguration cookieConfig,
		IOwnerService service
	)
	{
		_logger = logger;
		_cookieConfig = cookieConfig;
		_service = service;
	}

	[HttpPost]
	[Route("login")]
	public async Task<IActionResult> Login([FromBody] OwnerRequest body)
	{
		var response = false;
		var options = _cookieConfig.GetOptions();
		try
		{
			response = await _service.AuthenticateOwnerAsync(body.Password);
			if (response)
			{
				Response.Cookies.Append("Access", "Success", options);
				return Ok(new ApiResponse("Success", response));
			}
			else
			{
				return StatusCode(401, new ApiResponse("Failed", response));
			}
		}
		catch (Exception ex)
		{
			return StatusCode(500, new ApiResponse(ex.Message, response));
		}
	}

	[SessionVerifier]
	[HttpGet]
	[Route("logout")]
	public IActionResult Logout()
	{
		try
		{
			Response.Cookies.Delete("Access");
			return Ok(new ApiResponse("Logout", true));
		}
		catch (Exception ex)
		{
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}

	[SessionVerifier]
	[HttpPut]
	public async Task<IActionResult> UpdateMasterPassword([FromBody] OwnerRequest body)
	{
		try
		{
			await _service.SetupMasterPasswordAsync(body.Password);
			return Ok(new ApiResponse("Master password updated", true));
		}
		catch (Exception ex)
		{
			return StatusCode(500, new ApiResponse(ex.Message, false));
		}
	}
}