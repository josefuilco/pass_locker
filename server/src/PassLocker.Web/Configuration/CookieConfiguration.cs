namespace PassLocker.Web.Configuration;

public class CookieConfiguration
{
	private readonly IConfiguration configuration;

	public CookieConfiguration(IConfiguration configuration)
	{
		this.configuration = configuration;
	}

	public CookieOptions GetOptions()
	{
		return new CookieOptions()
		{
			Path = "/",
			HttpOnly = true,
			Expires = DateTimeOffset.UtcNow.AddMinutes(15),
			SameSite = SameSiteMode.None,
			Secure = true // Change when in production
		};
	}
}