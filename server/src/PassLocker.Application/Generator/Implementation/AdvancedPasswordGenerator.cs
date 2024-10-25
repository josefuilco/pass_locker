using System.Text;
using PassLocker.Application.Generator.Interfaces;

namespace PassLocker.Application.Generator.Implementation;

public class AdvancedPasswordGenerator : IPasswordGenerator
{
	#region Constants
	private const string chars = "abcdefghijkmnlopqrstuvwxyzABCDEFGHIJKMNLOPQRSTUVWXYZ!@#$%^&*()_+-={}[]:;'<>,.?/|~1234567890";
	private static readonly Random random = new();
	#endregion
	#region Attributes
	private readonly int length;
	#endregion

	public AdvancedPasswordGenerator(int length)
	{
		this.length = length > 0 ? length : 24;
	}

	public string GeneratePassword()
	{
		var passwordBuilder = new StringBuilder(length);
		for (int i = 0; i < length; i++)
		{
			passwordBuilder.Append(chars[random.Next(chars.Length)]);
		}
		return passwordBuilder.ToString();
	}
}