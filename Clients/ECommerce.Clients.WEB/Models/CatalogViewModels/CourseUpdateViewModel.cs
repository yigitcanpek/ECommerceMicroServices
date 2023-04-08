using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace ECommerce.Clients.WEB.Models.CatalogViewModels
{
    public class CourseUpdateViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Kurs İsmi")]
        [Required]

        public string Name { get; set; }
        [Display(Name = "Kurs Fiyat")]
        [Required]
        public decimal Price { get; set; }
        [Display(Name = "Kurs Açıklama")]
        [Required]
        public string Description { get; set; }
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public string Picture { get; set; }



        [Required]
        public FeatureViewModel Feature { get; set; }


        

        public string CategoryId { get; set; }

       

        [Display(Name = "Kurs Resmi")]
        [Required]
        public IFormFile PhotoFormFile { get; set; }
    }
}
