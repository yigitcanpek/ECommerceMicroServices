﻿using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Services.Catalog.Models
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime CreatedTime { get; set; }


        //OneToOne Relation
        public Feature Feature { get; set; }


        //ManyToOne
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CategoryId { get; set; }
        [BsonIgnore]
        public Category Category { get; set; }

        public Course()
        {
            CreatedTime = DateTime.Now;
        }
    }
}
