using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Rating.Data
{
    public class SkillService
    {
        private readonly SkillDbContext _db;
        public SkillService(SkillDbContext db)
        {
            _db = db;
        }

        public async Task<List<Category>> GetCategoriesAsync(string userId)
        {
            return await _db.Categories
                .Where(c => c.UserId == userId && !c.IsDeleted)
                .Include(c => c.Skills.Where(s => !s.IsDeleted))
                .ToListAsync();
        }

        public async Task<Category> AddCategoryAsync(string name, string userId)
        {
            var category = new Category { Name = name, UserId = userId };
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<Skill> AddSkillAsync(string name, double rating, int categoryId, string userId)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);
            if (category == null) throw new KeyNotFoundException("Category not found");
            var skill = new Skill { Name = name, Rating = rating, CategoryId = categoryId, UserId = userId };
            _db.Skills.Add(skill);
            await _db.SaveChangesAsync();
            return skill;
        }

        public async Task UpdateSkillRatingAsync(int skillId, double newRating, string userId)
        {
            var skill = await _db.Skills.FirstOrDefaultAsync(s => s.Id == skillId && s.UserId == userId);
            if (skill != null)
            {
                var oldRating = skill.Rating;
                if (oldRating != newRating)
                {
                    var history = new SkillHistory
                    {
                        SkillId = skillId,
                        OldRating = oldRating,
                        NewRating = newRating,
                        ChangedAt = DateTime.UtcNow,
                        UserId = userId
                    };
                    _db.SkillHistories.Add(history);
                }
                skill.Rating = newRating;
                await _db.SaveChangesAsync();
            }
        }

        public async Task EditSkillAsync(int skillId, string newName, string? notes, double? customMaxRating, string userId)
        {
            var skill = await _db.Skills.FirstOrDefaultAsync(s => s.Id == skillId && s.UserId == userId && !s.IsDeleted);
            if (skill != null)
            {
                skill.Name = newName;
                skill.Notes = notes;
                skill.CustomMaxRating = customMaxRating;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteSkillAsync(int skillId, string userId)
        {
            var skill = await _db.Skills.FirstOrDefaultAsync(s => s.Id == skillId && s.UserId == userId && !s.IsDeleted);
            if (skill != null)
            {
                skill.IsDeleted = true;
                await _db.SaveChangesAsync();
            }
        }

        public async Task RestoreSkillAsync(int skillId, string userId)
        {
            var skill = await _db.Skills.FirstOrDefaultAsync(s => s.Id == skillId && s.UserId == userId && s.IsDeleted);
            if (skill != null)
            {
                skill.IsDeleted = false;
                await _db.SaveChangesAsync();
            }
        }

        public async Task EditCategoryAsync(int categoryId, string newName, string userId)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId && !c.IsDeleted);
            if (category != null)
            {
                category.Name = newName;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteCategoryAsync(int categoryId, string userId)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId && !c.IsDeleted);
            if (category != null)
            {
                category.IsDeleted = true;
                await _db.SaveChangesAsync();
            }
        }

        public async Task RestoreCategoryAsync(int categoryId, string userId)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId && c.IsDeleted);
            if (category != null)
            {
                category.IsDeleted = false;
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<SkillHistory>> GetSkillHistoryAsync(int skillId, string userId)
        {
            return await _db.SkillHistories
                .Where(h => h.SkillId == skillId && h.UserId == userId)
                .OrderByDescending(h => h.ChangedAt)
                .ToListAsync();
        }

        public async Task<List<SkillHistory>> GetAllSkillHistoryAsync(string userId)
        {
            return await _db.SkillHistories
                .Where(h => h.UserId == userId)
                .Include(h => h.Skill)
                .ThenInclude(s => s.Category) 
                .OrderByDescending(h => h.ChangedAt)
                .ToListAsync();
        }
    }
}
