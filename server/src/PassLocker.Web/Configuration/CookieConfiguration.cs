namespace PassLocker.Web.Configuration;

public class CookieConfiguration
{
	private int cookieAccessIntent;
	private readonly IConfiguration configuration;

	public CookieConfiguration(IConfiguration configuration)
	{
		this.cookieAccessIntent = 0;
		this.configuration = configuration;
	}

	public CookieOptions GetAccessOptions()
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

	public CookieOptions GetBanOptions()
	{
		return new CookieOptions()
		{
			Path = "/",
			HttpOnly = true,
			Expires = DateTimeOffset.UtcNow.AddDays(1),
			SameSite = SameSiteMode.None,
			Secure = true // Change when in production
		};
	}

	public int GetCookieAccessIntent()
	{
		cookieAccessIntent++;
		return cookieAccessIntent;
	}
}