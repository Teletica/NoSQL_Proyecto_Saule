using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoSQL_Proyecto_Saule.Models
{
    public class Receta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("descripcionReceta")]
        public string descripcionReceta { get; set; }

        [Required]
        [BsonElement("fechaReceta")]
        public DateTime fechaReceta { get; set; }

        [Required]
        [BsonElement("prescriptorReceta")]
        public string prescriptorReceta { get; set; }

        [Required]
        [BsonElement("firmaPrescriptorReceta")]
        public string firmaPrescriptorReceta { get; set; }
    }
}