﻿namespace ECommerce.Clients.WEB.Models.FakePaymentViewModels
{
    public class PaymentInfoViewModel
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
