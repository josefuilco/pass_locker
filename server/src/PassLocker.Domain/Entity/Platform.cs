namespace PassLocker.Domain.Entity;

public class Platform
{
	public int Id { get; set; }
	public string? Name { get; set;}
	public string? UrlImage { get; set;}
	public bool IsActive { get; set; }
	public virtual Category? Category { get; set; }
	public virtual ICollection<Account>? Accounts { get; set; }
}