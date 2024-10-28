using Microsoft.EntityFrameworkCore;
using PassLocker.Domain.Entity;

namespace PassLocker.Infrastructure.Connection;

public class PassLockerDbContext : DbContext
{
	public DbSet<Owner>? Owner { get; set; }
	public DbSet<Category>? Category { get; set; }
	public DbSet<Platform>? Platform { get; set; }
	public DbSet<Tag>? Tag { get; set; }
	public DbSet<Account>? Account { get; set; }

	public PassLockerDbContext(DbContextOptions options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

	protected override void OnModelCreating(ModelBuilder model)
	{
		base.OnModelCreating(model);
		
		model.Entity<Owner>(owner => {
			owner.ToTable("Owner");

			owner.HasKey(o => o.Id)
			.HasName("owner_id");

			owner.Property(o => o.MasterPassword)
			.HasColumnName("owner_password")
			.HasColumnType("varchar")
			.HasMaxLength(120)
			.IsRequired();
		});

		model.Entity<Category>(category => {
			category.ToTable("Category");
			
			category.HasKey(c => c.Id)
			.HasName("category_id");

			category.Property(c => c.Name)
			.HasColumnName("category_name")
			.HasColumnType("varchar")
			.HasMaxLength(25)
			.IsRequired();

			category.Property(c => c.IsActive)
			.HasColumnName("category_state")
			.IsRequired();
		});

		model.Entity<Platform>(platform => {
			platform.ToTable("Platform");

			platform.HasKey(p => p.Id)
			.HasName("platform_id");

			platform.Property(p => p.Name)
			.HasColumnName("platform_name")
			.HasColumnType("varchar")
			.HasMaxLength(25)
			.IsRequired();

			platform.Property(p => p.UrlImage)
			.HasColumnName("platform_urlimage")
			.HasColumnType("varchar")
			.HasMaxLength(150);

			platform.Property(p => p.IsActive)
			.HasColumnName("platform_state")
			.IsRequired();

			platform.Property(p => p.CategoryId)
			.HasColumnName("category_id")
			.IsRequired();

			platform.HasOne(p => p.Category)
			.WithMany(P => P.Platforms)
			.HasForeignKey(p => p.CategoryId)
			.IsRequired();
		});

		model.Entity<Tag>(tag => {
			tag.ToTable("Tag");

			tag.HasKey(t => t.Id)
			.HasName("tag_id");

			tag.Property(t => t.Name)
			.HasColumnName("tag_name")
			.HasColumnType("varchar")
			.HasMaxLength(25)
			.IsRequired();

			tag.Property(t => t.Description)
			.HasColumnName("tag_description")
			.HasColumnType("varchar")
			.HasMaxLength(150)
			.IsRequired();

			tag.Property(t => t.IsActive)
			.HasColumnName("tag_state")
			.IsRequired();
		});

		model.Entity<Account>(account => {
			account.ToTable("Account");

			account.HasKey(a => a.Id)
			.HasName("account_id");

			account.Property(a => a.Name)
			.HasColumnName("account_name")
			.HasColumnType("varchar")
			.HasMaxLength(50)
			.IsRequired();

			account.Property(a => a.Password)
			.HasColumnName("account_password")
			.HasColumnType("varchar")
			.HasMaxLength(120)
			.IsRequired();

			account.Property(a => a.PlatformId)
			.HasColumnName("platform_id")
			.IsRequired();

			account.Property(a => a.TagId)
			.HasColumnName("tag_id")
			.IsRequired();

			account.HasOne(a => a.Platform)
			.WithMany(a => a.Accounts)
			.HasForeignKey(a => a.PlatformId)
			.IsRequired();

			account.HasOne(a => a.Tag)
			.WithMany(a => a.Accounts)
			.HasForeignKey(a => a.TagId)
			.IsRequired();
		});
	}
}