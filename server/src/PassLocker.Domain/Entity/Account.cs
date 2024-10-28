namespace PassLocker.Domain.Entity;

public class Account
{
	// Attributes
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Password { get; set; }

	// Foreign key
	public int PlatformId { get; set; }
	public int TagId { get; set; }

	// Navigation of Entity Framework
	public virtual Platform? Platform { get; set; }
	public virtual Tag? Tag { get; set; }
}