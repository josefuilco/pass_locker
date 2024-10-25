using PassLocker.Application.DTOs;
using PassLocker.Domain.Entity;

namespace PassLocker.Application.Service.Interfaces;

public interface ITagService
{
	Task SaveTagAsync(Tag tag);
	Task<IEnumerable<TagDTO>> GetAllTagsAsync();
	Task UpdateTagAsync(Tag tag);
	Task DeleteTagAsync(int tagId);
}