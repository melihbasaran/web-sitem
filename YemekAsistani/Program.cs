using Microsoft.EntityFrameworkCore;
using YemekAsistani.Data;

var builder = WebApplication.CreateBuilder(args);

// Veritabanı Servisini Ekle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controller ve View Servislerini Ekle
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Hata Yönetimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// --- STANDART ve GÜVENLİ AYAR ---
// Sunucunun resim, css gibi dosyaları açmasını sağlar.
app.UseStaticFiles(); 
// -------------------------------

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();