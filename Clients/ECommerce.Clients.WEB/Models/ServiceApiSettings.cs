namespace ECommerce.Clients.WEB.Models
{
    public class ServiceApiSettings
    {
        public string IdentityUrl { get; set; }
        public string GateWayUrl { get; set; }
        public string PhotoStockUrl { get; set; }
        public ServiceApi Catalog { get; set; }
        public ServiceApi PhotoStock { get; set; }
        public ServiceApi Basket { get; set; }
        public ServiceApi Discount { get; set; }
    }

    

    public class ServiceApi
    {
        public string path { get; set; }
    }
}
