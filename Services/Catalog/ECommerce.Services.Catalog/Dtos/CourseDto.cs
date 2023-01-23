using ECommerce.Services.Catalog.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Services.Catalog.Dtos
{
    internal class CourseDto
    {
        
        public string Id { get; set; }
        public string Name { get; set; }
      
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        
        public DateTime CreatedTime { get; set; }


        //OneToOne Relation
        public FeatureDto Feature { get; set; }


        //ManyToOne
       
        public string CategoryId { get; set; }
       
        public CategoryDto Category { get; set; }
    }
}
