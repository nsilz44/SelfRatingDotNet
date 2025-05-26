namespace Rating.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Skill> Skills { get; set; } = new();
        public string UserId { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false; // Soft delete
    }

    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Rating { get; set; } // 0.0 to 10.0, increments of 0.1
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? Notes { get; set; } // Optional notes for customization
        public double? CustomMaxRating { get; set; } // Optional custom max rating
        public bool IsDeleted { get; set; } = false; // Soft delete
    }

    public class SkillHistory
    {
        public int Id { get; set; }
        public int SkillId { get; set; }
        public Skill? Skill { get; set; }
        public double OldRating { get; set; }
        public double NewRating { get; set; }
        public DateTime ChangedAt { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
