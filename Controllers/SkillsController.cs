using Microsoft.AspNetCore.Mvc;
using Rating.Data;

namespace Rating.Controllers
{
    public class SkillsController : Controller
    {
        private readonly SkillService _skillService;

        public SkillsController(SkillService skillService)
        {
            _skillService = skillService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = "1";
            var categories = await _skillService.GetCategoriesAsync(userId);
            var allSkillHistory = await _skillService.GetAllSkillHistoryAsync(userId);

            var viewModel = new SkillsIndexViewModel
            {
                Categories = categories,
                AllSkillHistory = allSkillHistory
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(string name)
        {
            var userId = "1";
            if (userId == null)
                return Challenge();

            if (!string.IsNullOrWhiteSpace(name))
            {
                await _skillService.AddCategoryAsync(name, userId);
                TempData["Message"] = "Category added successfully.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill(string name, double rating, int categoryId)
        {
            var userId = "1";
            if (userId == null)
                return Challenge();

            if (!string.IsNullOrWhiteSpace(name) && rating >= 0 && rating <= 10)
            {
                await _skillService.AddSkillAsync(name, rating, categoryId, userId);
                TempData["Message"] = "Skill added successfully.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSkillRating(int id, double rating)
        {
            var userId = "1";
            if (userId == null)
                return Challenge();

            if (rating >= 0 && rating <= 10)
            {
                await _skillService.UpdateSkillRatingAsync(id, rating, userId);
                TempData["Message"] = "Skill rating updated successfully.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var userId = "1";
            if (userId == null)
                return Challenge();

            await _skillService.DeleteSkillAsync(id, userId);
            TempData["Message"] = "Skill deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var userId = "1";
            if (userId == null)
                return Challenge();

            await _skillService.DeleteCategoryAsync(id, userId);
            TempData["Message"] = "Category deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditSkill(int id)
        {
            var userId = "1";
            if (userId == null)
                return Challenge();

            var categories = await _skillService.GetCategoriesAsync(userId);
            var skill = categories.SelectMany(c => c.Skills).FirstOrDefault(s => s.Id == id);
            if (skill == null)
                return NotFound();

            return View(skill);
        }

        [HttpPost]
        public async Task<IActionResult> EditSkill(int id, string name, string notes, double? customMaxRating)
        {
            var userId = "1";
            if (userId == null)
                return Challenge();

            await _skillService.EditSkillAsync(id, name, notes, customMaxRating, userId);
            TempData["Message"] = "Skill updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var userId = "1";
            if (userId == null)
                return Challenge();

            var categories = await _skillService.GetCategoriesAsync(userId);
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(int id, string name)
        {
            var userId = "1";
            if (userId == null)
                return Challenge();

            await _skillService.EditCategoryAsync(id, name, userId);
            TempData["Message"] = "Category updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> SkillHistory(int id)
        {
            var userId = "1";
            if (userId == null)
                return Challenge();

            var history = await _skillService.GetSkillHistoryAsync(id, userId);
            var categories = await _skillService.GetCategoriesAsync(userId);
            var skill = categories.SelectMany(c => c.Skills).FirstOrDefault(s => s.Id == id);

            if (skill == null)
                return NotFound();

            ViewData["SkillName"] = skill.Name;
            return View(history);
        }

        [HttpGet]
        public async Task<IActionResult> AllSkillHistory()
        {
            var userId = "1";
            if (userId == null)
                return Challenge();

            var history = await _skillService.GetAllSkillHistoryAsync(userId);
            var categories = await _skillService.GetCategoriesAsync(userId);

            ViewData["SkillName"] = userId.ToString();
            return View(history);
        }
    }
}
