using System.Security.Cryptography;
using PassLocker.Domain.Service;

namespace PassLocker.Infrastructure.Service;

public class AesEncryptService : IEncryptService
{
	#region Constants
	private const int keySize = 16;
	private const int ivSize = 16;
	private const int base64IvLength = 24;
	#endregion

	#region Attributes
	private readonly byte[] key;
	private readonly byte[] iv;
	private readonly int base64KeyLength;
	#endregion

	public AesEncryptService()
	{
		key = new byte[keySize];
		iv = new byte[ivSize];
		base64KeyLength = (int) Math.Ceiling((decimal) keySize / 3) * 4;
	}

	#region Methods
	private void GenerateKeyAndIv()
	{
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(key);
		rng.GetBytes(iv);
	}

	private (byte[] key, byte[] iv) ExtractKeyAndIv(string passwordWithKeyAndIv)
	{
		var key = Convert.FromBase64String(passwordWithKeyAndIv[..base64KeyLength]);
		var iv = Convert.FromBase64String(passwordWithKeyAndIv[base64KeyLength..(base64KeyLength + base64IvLength)]);
		return (key, iv);
	}

	private byte[] ExtractCipheredPassword(string passwordWithKeyAndIv)
	{
		return Convert.FromBase64String(passwordWithKeyAndIv[(base64KeyLength + base64IvLength)..]);
	}

	private string PasswordWithKeyAndIv(byte[] cipheredPassword)
	{
		var keyBase64 = Convert.ToBase64String(key);
		var ivBase64 = Convert.ToBase64String(iv);
		var cipheredPasswordBase64 = Convert.ToBase64String(cipheredPassword);
		return $"{keyBase64}{ivBase64}{cipheredPasswordBase64}";
	}

	public string Encode(string password)
	{
		GenerateKeyAndIv();
		using var aes = Aes.Create();
		var encryptor = aes.CreateEncryptor(key, iv);
		using var memoryStream = new MemoryStream();
		using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
		using var streamWriter = new StreamWriter(cryptoStream);
		streamWriter.Write(password);
		streamWriter.Flush();
		cryptoStream.FlushFinalBlock();
		return PasswordWithKeyAndIv(memoryStream.ToArray());
	}
	public string Decode(string passwordWithKeyAndIv)
	{
		var (key, iv) = ExtractKeyAndIv(passwordWithKeyAndIv);
		var cipheredPassword = ExtractCipheredPassword(passwordWithKeyAndIv);
		using var aes = Aes.Create();
		var decryptor = aes.CreateDecryptor(key, iv);
		using var memoryStream = new MemoryStream(cipheredPassword);
		var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
		using var streamReader = new StreamReader(cryptoStream);
		return streamReader.ReadToEnd();
	}
	#endregion
}