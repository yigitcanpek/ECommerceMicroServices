using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ECommerce.Clients.WEB.Models.CatalogViewModels
{
    public class FeatureViewModel
    {
        [Display(Name = "Kurs Süresi")]
        [Required]
        public int Duration { get; set; }
    }
}
