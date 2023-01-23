namespace ECommerce.Services.Catalog.DesignPatterns
{
     internal interface IDatabaseOptions
    {
        public string CourseCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        
    }
}
