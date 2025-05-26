using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace nosqllllll.Models
{
    public class Telebe
    {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public ObjectId Id { get; set; }

            [BsonElement("Ad")]
            public string Ad { get; set; }

            [BsonElement("Soyad")]
            public string Soyad { get; set; }

            [BsonElement("AtaAd")]
            public string AtaAd { get; set; }

            [BsonElement("Qrup")]
            public string Qrup { get; set; }

            [BsonElement("Cins")]
            public string Cins { get; set; }

            [BsonElement("Ortalama")]
            public double Ortalama { get; set; }

            [BsonElement("Teqaud")]
            public string Teqaud { get; set; }
    }
}
