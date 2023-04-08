using System.ComponentModel.DataAnnotations;

namespace ECommerce.Clients.WEB.Models.CatalogViewModels
{
    public class CourseCreateInputViewModel
    {
        [Display(Name="Kurs İsimi")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Kurs Fiyatı")]
        [Required]
        public decimal Price { get; set; }
        [Display(Name = "Kurs Açıklaması")]
        [Required]
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }

        public DateTime CreatedTime { get; set; }


        
        public FeatureViewModel Feature { get; set; }



        [Display(Name = "Kurs Kategori")]
        [Required]
        public string CategoryId { get; set; }
        [Display(Name = "Kurs Resmi")]
        [Required]
        public IFormFile PhotoFormFile { get; set; }
    }
}
