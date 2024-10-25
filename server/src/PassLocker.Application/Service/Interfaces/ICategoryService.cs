using PassLocker.Application.DTOs;
using PassLocker.Domain.Entity;

namespace PassLocker.Application.Service.Interfaces;

public interface ICategoryService
{
	Task SaveCategoryAsync(Category category);
	Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
	Task UpdateCategoryAsync(Category category);
	Task DeleteCategoryAsync(int categoryId);
}