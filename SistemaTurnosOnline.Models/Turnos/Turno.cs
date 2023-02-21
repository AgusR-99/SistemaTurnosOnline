using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SistemaTurnosOnline.Shared.Turnos
{
    public class Turno
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Descripcion { get; set; }
        public string CarreraId { get; set; }
        public bool Estado { get; set; }
        public long OrdenEnCola { get; set; }
    }
}
