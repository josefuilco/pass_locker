namespace PassLocker.Domain.Entity;

public class Platform
{
	// Attributes
	public int Id { get; set; }
	public string? Name { get; set;}
	public string? UrlImage { get; set;}
	public bool IsActive { get; set; }

	// Foreign Key
	public int CategoryId { get; set; }

	// Navigation of Entity Framework
	public virtual Category? Category { get; set; }
	public virtual ICollection<Account>? Accounts { get; set; }
}