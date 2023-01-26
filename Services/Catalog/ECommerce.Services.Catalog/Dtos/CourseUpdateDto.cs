namespace ECommerce.Services.Catalog.Dtos
{
    public class CourseUpdateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }



        //OneToOne Relation
        public FeatureDto Feature { get; set; }


        //ManyToOne

        public string CategoryId { get; set; }

        public CategoryDto Category { get; set; }
    }
}
