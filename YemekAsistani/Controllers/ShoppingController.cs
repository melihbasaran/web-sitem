using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekAsistani.Data;
using System.Security.Claims; 
using Microsoft.AspNetCore.Authorization; 
using YemekAsistani.Models;

namespace YemekAsistani.Controllers
{
    [Authorize] 
    public class ShoppingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Listeyi Getir
      // GET: Shopping
        public async Task<IActionResult> Index()
        {
            // Giriş yapan kullanıcının ID'sini bul
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Sadece BU kullanıcıya ait olanları getir
            // Eğer userId boşsa (admin değilse) boş liste dönsün
            if (userId == null)
            {
                return View(new List<ShoppingItem>());
            }

            var myItems = await _context.ShoppingItems
                                        .Where(x => x.OwnerId == userId) 
                                        .ToListAsync();

            return View(myItems);
        }

        // 2. Yeni Ürün Ekle
        [HttpPost]
        public async Task<IActionResult> Add(string ItemName)
        {
            if (!string.IsNullOrWhiteSpace(ItemName))
            {
                // 1. Giriş yapan kişinin kimliğini al
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // 2. Yeni malzemeyi oluştururken "Sahibi = Ben" de
                _context.ShoppingItems.Add(new ShoppingItem 
                { 
                    ItemName = ItemName, 
                    IsChecked = false,
                    OwnerId = userId // 👈 İŞTE SİHİRLİ DOKUNUŞ BURASI
                });

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