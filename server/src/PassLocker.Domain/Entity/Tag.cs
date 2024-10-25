namespace PassLocker.Domain.Entity;

public class Tag
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public bool IsActive { get; set; }
	public virtual ICollection<Account>? Accounts { get; set; }
}