using PassLocker.Application.DTOs;
using PassLocker.Application.Service.Interfaces;
using PassLocker.Domain.Entity;
using PassLocker.Domain.Repository;

namespace PassLocker.Application.Service.Implementation;

public class TagService : ITagService
{
	private readonly ITagRepository repository;

	public TagService(ITagRepository repository)
	{
		this.repository = repository;
	}

	public async Task DeleteTagAsync(int tagId)
	{
		await repository.RemoveTagAsync(tagId);
	}

	public async Task<IEnumerable<TagDTO>> GetAllTagsAsync()
	{
		var tags = await repository.GetAllTagsAsync();
		return tags.Select(t => new TagDTO(t.Id, t.Name ?? "Unknown", t.Description ?? "Unknown"));
	}

	public async Task SaveTagAsync(Tag tag)
	{
		await repository.SaveTagAsync(tag);
	}

	public async Task UpdateTagAsync(Tag tag)
	{
		await repository.UpdateTagAsync(tag);
	}
}