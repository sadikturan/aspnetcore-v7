using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class Product
    {
        [Display(Name="Urun Id")]
        public int ProductId { get; set; }

        [Display(Name="Urun Adı")]
        public string Name { get; set; } = string.Empty;

        [Display(Name="Fiyat")]
        public decimal Price { get; set; }

        [Display(Name="Resim")]
        public string Image { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
    }
}

// 1 iphone 14 1
// 2 iphone 15 1