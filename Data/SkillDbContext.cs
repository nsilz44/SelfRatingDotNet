using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Rating.Data
{
    public class SkillDbContext : IdentityDbContext
    {
        public SkillDbContext(DbContextOptions<SkillDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillHistory> SkillHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Skills)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SkillHistory>()
                .HasOne(h => h.Skill)
                .WithMany()
                .HasForeignKey(h => h.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            // Soft delete query filters
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Skill>().HasQueryFilter(s => !s.IsDeleted);

            // Configure new properties
            modelBuilder.Entity<Skill>().Property(s => s.Notes).HasMaxLength(1024);
            modelBuilder.Entity<Skill>().Property(s => s.CustomMaxRating);
        }
    }
}
