using Microsoft.AspNetCore.Mvc;

namespace PassLocker.Web.Controller;

[ApiController]
[Route("api/v1/auth")]
public class AuthenticationController : ControllerBase
{
	private readonly ILogger<AuthenticationController> _logger;

	public AuthenticationController(ILogger<AuthenticationController> logger)
	{
		_logger = logger;
	}

	[HttpGet]
	[Route("ping")]
	public IActionResult Ping()
	{
		return Ok("Pong");
	}
}