using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekAsistani.Data;
using YemekAsistani.Models;
using System.Linq;
using System.Threading.Tasks;

namespace YemekAsistani.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ---------------------------------------------------------
        // HERKESİN GÖREBİLDİĞİ KISIMLAR (KİLİTSİZ)
        // ---------------------------------------------------------

        // 1. LİSTELEME VE FİLTRELEME
        public async Task<IActionResult> Index(string[] malzemeler)
        {
            var recipes = await _context.Recipes.ToListAsync();

            // Eğer filtre seçildiyse ona göre ele
            if (malzemeler != null && malzemeler.Length > 0)
            {
                recipes = recipes.Where(r => malzemeler.Any(secilen => r.Ingredients.Contains(secilen))).ToList();
            }

            return View(recipes);
        }

        // 2. DETAY GÖRME
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null) return NotFound();

            return View(recipe);
        }

        // ---------------------------------------------------------
        // 🔒 YÖNETİCİ BÖLGESİ (SADECE ADMIN)
        // ---------------------------------------------------------

        // 3. YENİ EKLEME - SAYFAYI AÇAR
        public IActionResult Create()
        {
            // KİLİT: Admin değilse ana sayfaya postala
            if (User.Identity.Name != "admin@admin") return RedirectToAction(nameof(Index));
            
            return View();
        }

        // 4. YENİ EKLEME - KAYDEDER
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ImageUrl,PrepTime,Servings,Ingredients")] Recipe recipe)
        {
            if (User.Identity.Name != "admin@admin") return RedirectToAction(nameof(Index));

            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // 5. GÜNCELLEME (EDİT) - SAYFAYI AÇAR 🆕
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.Name != "admin@admin") return RedirectToAction(nameof(Index)); // Kilit
            
            if (id == null) return NotFound();

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound();
            
            return View(recipe);
        }

        // 6. GÜNCELLEME (EDİT) - KAYDEDER 🆕
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageUrl,PrepTime,Servings,Ingredients")] Recipe recipe)
        {
            if (User.Identity.Name != "admin@admin") return RedirectToAction(nameof(Index)); // Kilit

            if (id != recipe.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Recipes.Any(e => e.Id == recipe.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // 7. SİLME İŞLEMİ
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.Name != "admin@admin") return RedirectToAction(nameof(Index)); // Kilit

            if (id == null) return NotFound();

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}