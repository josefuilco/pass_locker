using System.Text;
using PassLocker.Application.Generator.Interfaces;

namespace PassLocker.Application.Generator.Implementation;

public class BasicPasswordGenerator : IPasswordGenerator
{
	private const string chars = "abcdefghijkMNLOPQRSTUVWXYZ@*_=1234567890";
	private static readonly Random random = new();

	public string GeneratePassword()
	{
		var passwordBuilder = new StringBuilder(16);
		for (int i = 0; i < 16; i++)
		{
			passwordBuilder.Append(chars[random.Next(chars.Length)]);
		}
		return passwordBuilder.ToString();
	}
}