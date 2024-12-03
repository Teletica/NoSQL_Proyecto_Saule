using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoSQL_Proyecto_Saule.Models
{
    public class Proveedor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 

        [Required]
        [BsonElement("nombreProveedor")]
        public string NombreProveedor { get; set; }

        [BsonElement("apellidoUnoProveedor")]
        public string ApellidoUnoProveedor { get; set; }

        [BsonElement("apellidoDosProveedor")]
        public string ApellidoDosProveedor { get; set; }

        [BsonElement("companyProveedor")]
        public string CompanyProveedor { get; set; }

        [BsonElement("contactoProveedor")]
        public ContactoProveedor ContactoProveedor { get; set; } 
    }

    public class ContactoProveedor
    {
        [BsonElement("numTelefonoUnoProveedor")]
        public string NumTelefonoUnoProveedor { get; set; }

        [BsonElement("numTelefonoDosProveedor")]
        public string NumTelefonoDosProveedor { get; set; }

        [BsonElement("correoProveedor")]
        public string CorreoProveedor { get; set; }
    }
}
