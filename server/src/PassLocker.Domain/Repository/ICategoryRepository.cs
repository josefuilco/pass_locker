using PassLocker.Domain.Entity;

namespace PassLocker.Domain.Repository;

public interface ICategoryRepository
{
	Task SaveCategoryAsync(Category category);
	Task<IEnumerable<Category>> GetAllCategoriesAsync();
	Task UpdateCategoryAsync(Category category);
	Task RemoveCategoryAsync(int categoryId);
}