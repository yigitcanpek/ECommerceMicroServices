using System.ComponentModel.DataAnnotations;

namespace ECommerce.Clients.WEB.Models.OrderViewModels
{
    public class CheckOutViewModel
    {
        [Display(Name ="İl")]
        public string Province { get; private set; }
        [Display(Name = "İlçe")]
        public string District { get; private set; }
        [Display(Name = "Sokak")]
        public string Street { get; private set; }
        [Display(Name = "Posta Kodu")]
        public string ZipCode { get; private set; }
        [Display(Name = "Adres")]
        public string Line { get; private set; }


        [Display(Name = "Kart isim soy isim")]
        public string CardName { get; set; }
        [Display(Name = "Kart numarası")]
        public string CardNumber { get; set; }
        [Display(Name = "Kart son kullanma tarihi")]
        public string Expiration { get; set; }
        [Display(Name = "CVV kodu")]
        public string CVV { get; set; }
        [Display(Name = "Toplam ücret")]
        public decimal TotalPrice { get; set; }
    }
}
