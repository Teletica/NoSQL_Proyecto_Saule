using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace NoSQL_Proyecto_Saule.Models
{
    public class Rol
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("nombreRol")]
        public string nombreRol { get; set; }

        [Required]
        [BsonElement("descripcionRol")]
        public string descripcionRol { get; set; }

        [Required]
        [BsonElement("estadoRol")]
        public bool estadoRol { get; set; }


    }
}