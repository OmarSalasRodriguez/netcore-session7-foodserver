using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Food.Models
{
	public class FoodModel
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string Name { get; set; } = null!;

        [BsonElement("Description")]
        [JsonPropertyName("Description")]
        public string Description { get; set; } = null!;

        [BsonElement("Price")]
        [JsonPropertyName("Price")]
        public decimal Price { get; set; }
    }
}

