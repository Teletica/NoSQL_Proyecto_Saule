using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace NoSQL_Proyecto_Saule.Models
{
    public class Grupo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("nombreGrupo")]
        public string nombrGrupo { get; set; }

        [Required]
        [BsonElement("descripcionGrupo")]
        public string descripcionGrupo { get; set; }

        [Required]
        [BsonElement("Categoria")]
        public IdContainer IdCategoria { get; set; } 

        public class IdContainer
        {
            [BsonElement("_id")]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }
        }
    }
}
