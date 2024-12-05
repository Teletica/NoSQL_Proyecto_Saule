using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace NoSQL_Proyecto_Saule.Models
{
    public class Facturas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("idCompra")]
        public string IdCompra { get; set; } // Cambiado de BsonValue a string

        [Required]
        [BsonElement("idVenta")]
        public string IdVenta { get; set; } // Cambiado de BsonValue a string
    }
}
