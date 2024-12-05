using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NoSQL_Proyecto_Saule.Models
{
    public class Compras
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("fechaCompra")]
        public DateTime FechaCompra { get; set; } // Corregido a PascalCase

        [Required]
        [BsonElement("productosCompra")]
        public List<string> ProductosCompra { get; set; } // Cambiado de BsonValue a List<string>
    }
}
