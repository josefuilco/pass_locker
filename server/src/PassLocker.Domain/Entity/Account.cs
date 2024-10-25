namespace PassLocker.Domain.Entity;

public class Account
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Password { get; set; }
	public virtual Platform? Platform { get; set; }
	public virtual Tag? Tag { get; set; }
}