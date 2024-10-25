using Microsoft.EntityFrameworkCore;
using PassLocker.Domain.Entity;
using PassLocker.Domain.Repository;
using PassLocker.Infrastructure.Connection;

namespace PassLocker.Infrastructure.Repository;

public class EfTagRepository : ITagRepository
{
	private readonly PassLockerDbContext dbContext;

	public EfTagRepository(PassLockerDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task<IEnumerable<Tag>> GetAllTagsAsync()
	{
		if (dbContext.Tag == null) throw new Exception("Tag table without values");
		var tags = await dbContext.Tag.Where(t => t.IsActive == true).ToListAsync();
		return tags;
	}

	public async Task RemoveTagAsync(int tagId)
	{
		if (dbContext.Tag == null) throw new Exception("Tag table without values");
		var currentTag = await dbContext.Tag.Where(t => t.Id == tagId).FirstAsync();
		currentTag.IsActive = false;
		await dbContext.SaveChangesAsync();
	}

	public async Task SaveTagAsync(Tag tag)
	{
		await dbContext.Tag!.AddAsync(tag);
		await dbContext.SaveChangesAsync();
	}

	public async Task UpdateTagAsync(Tag tag)
	{
		if (dbContext.Tag == null) throw new Exception("Tag table without values");
		var currentTag = await dbContext.Tag.Where(t => t.Id == tag.Id).FirstAsync();
		currentTag.Name = tag.Name;
		currentTag.Description = tag.Description;
		await dbContext.SaveChangesAsync();
	}
}