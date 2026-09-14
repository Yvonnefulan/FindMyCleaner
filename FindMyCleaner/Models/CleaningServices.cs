using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FindMyCleaner.Model
{
    public class CleaningServices
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string serviceProvider { get; set; } = string.Empty;
        public string serviceName { get; set; } = string.Empty;
        public string serviceType { get; set; } = string.Empty;
        public string suburb { get; set; } = string.Empty;
        public double priceFrom { get; set; }
        public double minDurationHours { get; set; }
        public bool isAvailable { get; set; }
        public bool isNightShiftAvailable { get; set; }
        public List<string> availableDay { get; set; } = new();
        public DateTime createdDate { get; set; }
        public List<string> keywords { get; set; } = new();
    }
}
