using System.ComponentModel.DataAnnotations;

namespace YemekAsistani.Models
{
    public class ShoppingItem
    {
        public int Id { get; set; }

        [Required]
        public string ItemName { get; set; } = ""; // Alınacak ürün (Örn: Süt)

        public bool IsChecked { get; set; } // Alındı mı? (Tik işareti için)
    }
}