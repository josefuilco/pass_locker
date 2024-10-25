using Microsoft.EntityFrameworkCore;
using PassLocker.Domain.Entity;
using PassLocker.Domain.Repository;
using PassLocker.Infrastructure.Connection;

namespace PassLocker.Infrastructure.Repository;

public class EfCategoryRepository : ICategoryRepository
{
	private readonly PassLockerDbContext dbContext;

	public EfCategoryRepository(PassLockerDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
	{
		var categories = await dbContext.Category!.Where(c => c.IsActive == true).ToListAsync();
		return categories;
	}

	public async Task RemoveCategoryAsync(int categoryId)
	{
		var currentCategory = await dbContext.Category!.Where(c => c.Id == categoryId).FirstOrDefaultAsync()
			?? throw new Exception("Category not found for remove");
		currentCategory.IsActive = false;
		await dbContext.SaveChangesAsync();
	}

	public async Task SaveCategoryAsync(Category category)
	{
		await dbContext.Category!.AddAsync(category);
		await dbContext.SaveChangesAsync();
	}

	public async Task UpdateCategoryAsync(Category category)
	{
		var currentCategory = await dbContext.Category!.Where(c => c.Id == category.Id).FirstOrDefaultAsync()
			?? throw new Exception("Category not found for update");
		currentCategory.Name = category.Name;
		await dbContext.SaveChangesAsync();
	}
}