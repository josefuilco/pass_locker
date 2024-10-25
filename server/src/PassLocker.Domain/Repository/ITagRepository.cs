using PassLocker.Domain.Entity;

namespace PassLocker.Domain.Repository;

public interface ITagRepository
{
	Task SaveTagAsync(Tag tag);
	Task<IEnumerable<Tag>> GetAllTagsAsync();
	Task UpdateTagAsync(Tag tag);
	Task RemoveTagAsync(int tagId);
}