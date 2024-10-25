namespace PassLocker.Domain.Service;

public interface IEncryptService
{
	string Encode(string password);
	string Decode(string encodedPassword);
}