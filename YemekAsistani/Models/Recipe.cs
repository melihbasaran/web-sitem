using System.ComponentModel.DataAnnotations;

namespace YemekAsistani.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = ""; // Yemek Adı

        public string Description { get; set; } = ""; // Açıklama

        public string ImageUrl { get; set; } = ""; // Resim Linki

        public int PrepTime { get; set; } // Süre (dk)

        public int Servings { get; set; } // Kaç Kişilik

        public string Ingredients { get; set; } = ""; // Malzemeler (Aramayı burada yapacağız)
    }
}