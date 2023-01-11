using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SistemaTurnosOnline.Shared
{
    public class Profesor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public bool Estado { get; set; }
        public string Password { get; set; }
        public List<string>? CarrerasId { get; set; }
    }
}
