using System.Security.Cryptography;
using PassLocker.Domain.Service;

namespace PassLocker.Infrastructure.Service;

public class AesEncryptService : IEncryptService
{
	private static RandomNumberGenerator? rng;
	private readonly byte[] key;
	private readonly byte[] iv;

	public AesEncryptService()
	{
		key = new byte[16];
		iv = new byte[16];
	}

	private void GenerateKeyAndIv()
	{
		rng ??= RandomNumberGenerator.Create();
		rng.GetBytes(key);
		rng.GetBytes(iv);
	}

	private static Dictionary<byte[], byte[]> ExtractKeyAndId(string passwordWithKeyAndIv)
	{
		var keyAndIv = new Dictionary<byte[], byte[]>
        {
            {
                Convert.FromBase64String(passwordWithKeyAndIv[24..]),
                Convert.FromBase64String(passwordWithKeyAndIv.Substring(24, 48))
            }
        };
		return keyAndIv;
	}

	private static byte[] ExtractEncodedPassword(string passwordWithKeyAndIv)
	{
		return Convert.FromBase64String(passwordWithKeyAndIv.Substring(48, passwordWithKeyAndIv.Length));
	}

	private string PasswordWithKeyAndIv(byte[] cipheredPassword)
	{
		var bytes = new List<byte[]>
		{
			key,
			iv,
			cipheredPassword
		};
		var result = from value in bytes select Convert.ToBase64String(value);
		return string.Join("", result);
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
		return PasswordWithKeyAndIv(memoryStream.ToArray());
	}
	public string Decode(string passwordWithKeyAndIv)
	{
		var decryptorParams = ExtractKeyAndId(passwordWithKeyAndIv).FirstOrDefault();
		var cipheredPassword = ExtractEncodedPassword(passwordWithKeyAndIv);
		using var aes = Aes.Create();
		var decryptor = aes.CreateDecryptor(decryptorParams.Key, decryptorParams.Value);
		using var memoryStream = new MemoryStream(cipheredPassword);
		var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
		using var streamReader = new StreamReader(cryptoStream);
		return streamReader.ReadToEnd();
	}
}