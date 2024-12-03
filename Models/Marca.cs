using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NoSQL_Proyecto_Saule.Models
{
    public class Marca
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("nombreMarca")]
        public string nombreMarca { get; set; }

        [Required]
        [BsonElement("imgLogoMarca")]
        public string imgLogoMarca { get; set; }

        [Required]
        [BsonElement("estadoMarca")]
        public bool estadoMarca { get; set; }
    }
}