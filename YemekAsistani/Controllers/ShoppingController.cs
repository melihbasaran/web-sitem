using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekAsistani.Data;
using YemekAsistani.Models;

namespace YemekAsistani.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Listeyi Getir
        public async Task<IActionResult> Index()
        {
            var items = await _context.ShoppingItems.ToListAsync();
            return View(items);
        }

        // 2. Yeni Ürün Ekle
        [HttpPost]
        public async Task<IActionResult> Add(string ItemName)
        {
            if (!string.IsNullOrWhiteSpace(ItemName))
            {
                _context.ShoppingItems.Add(new ShoppingItem { ItemName = ItemName, IsChecked = false });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // 3. Yapıldı/Yapılmadı İşaretle (Tik Atma)
        public async Task<IActionResult> Toggle(int id)
        {
            var item = await _context.ShoppingItems.FindAsync(id);
            if (item != null)
            {
                item.IsChecked = !item.IsChecked; // Tersine çevir (True ise False, False ise True yap)
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // 4. Ürünü Sil
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.ShoppingItems.FindAsync(id);
            if (item != null)
            {
                _context.ShoppingItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}