using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekAsistani.Data;
using YemekAsistani.Models;

namespace YemekAsistani.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Garson işe başlıyor: Veritabanı anahtarını (context) eline alıyor
        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Ana Sayfa (Listeleme ve Arama)
// "malzemeler" parametresi, seçtiğin kutucuklardan gelen veridir.
        public async Task<IActionResult> Index(string[] malzemeler)
        {
            // 1. Önce bütün yemekleri veritabanından çekelim
            var recipes = await _context.Recipes.ToListAsync();

            // 2. Eğer kullanıcı bir şeyler seçmişse FİLTRELEME yapalım
            if (malzemeler != null && malzemeler.Length > 0)
            {
                // Şöyle bir mantık kuruyoruz:
                // Yemeğin malzemeleri içinde, kullanıcının seçtiği malzemelerden HERHANGİ BİRİ geçiyor mu?
                recipes = recipes.Where(r => malzemeler.Any(secilen => r.Ingredients.Contains(secilen))).ToList();
            }

            // 3. Sonuçları sayfaya gönder
            return View(recipes);
        }
        // DETAY SAYFASI İÇİN KOD
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recipe == null) return NotFound();

            return View(recipe);
        }
        // ---------------------------------------------------------
        // YENİ EKLENECEK KISIM: TARİF EKLEME SAYFASI (GET)
        // ---------------------------------------------------------
        public IActionResult Create()
        {
            return View();
        }

        // ---------------------------------------------------------
        // YENİ EKLENECEK KISIM: TARİFİ KAYDETME İŞLEMİ (POST)
        // ---------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ImageUrl,PrepTime,Servings,Ingredients")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }
        // ---------------------------------------------------------
        // TEK TIKLA HAZIR YEMEK YÜKLEME KODU (SEEDING)
        // ---------------------------------------------------------
        public IActionResult HazirYemekleriYukle()
        {
            // Eğer zaten içeride yemek varsa ekleme yapma
            if (_context.Recipes.Any())
            {
                return Content("Veritabanında zaten yemekler var! Tekrar eklemedim.");
            }

            // İşte hazır tarifler listesi
            var tarif1 = new Recipe
            {
                Title = "Karnıyarık",
                Description = "Patlıcan ve kıymanın efsane buluşması. Türk mutfağının vazgeçilmezi.",
                PrepTime = 45,
                Servings = 4,
                Ingredients = "Patlıcan, Kıyma, Domates, Soğan, Biber"
            };

            var tarif2 = new Recipe
            {
                Title = "Mercimek Çorbası",
                Description = "Kış günlerinin şifası, bol limonlu içilmesi tavsiye edilir.",
                PrepTime = 20,
                Servings = 6,
                Ingredients = "Mercimek, Soğan, Havuç, Patates, Tuz"
            };

            var tarif3 = new Recipe
            {
                Title = "Menemen",
                Description = "Soğanlı mı soğansız mı tartışmasını bitiren lezzet. Bekarların kral yemeği.",
                PrepTime = 15,
                Servings = 2,
                Ingredients = "Yumurta, Domates, Biber, Soğan"
            };

            // Hepsini sepete at
            _context.Recipes.AddRange(tarif1, tarif2, tarif3);
            
            // Veritabanına kaydet
            _context.SaveChanges();

            return Content("✅ Başarılı! Karnıyarık, Mercimek ve Menemen veritabanına eklendi. Ana sayfaya dönebilirsin.");
        }
        // SİLME İŞLEMİ (Tek tıkla siler)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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