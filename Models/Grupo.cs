using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoSQL_Proyecto_Saule.Models
{
    public class Grupo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        [Required]
        [BsonElement("nombreGrupo")]
        public string nombrGrupo { get; set; }

        [Required]
        [BsonElement("descripcionGrupo")]
        public string descripcionGrupo { get; set; }

        [Required]
        [BsonElement("Categoria")]  
        public BsonValue IdCategoria { get; set; }  

    }
}