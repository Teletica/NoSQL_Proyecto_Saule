using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NoSQL_Proyecto_Saule.Models
{
    public class Categoria
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("nombreCategoria")]
        public string nombreCategoria { get; set; }

        [Required] 
        [BsonElement("estadoCategoria")]
        public bool estadoCategoria { get; set; }


    }
}