using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SistemaTurnosOnline.Shared
{
    public class Carrera
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }
}
