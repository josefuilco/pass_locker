namespace PassLocker.Domain.Entity;

public class Category
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public bool IsActive { get; set; }
	public virtual ICollection<Platform>? Platforms { get;}
}