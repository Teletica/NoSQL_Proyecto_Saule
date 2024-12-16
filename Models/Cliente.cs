using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoSQL_Proyecto_Saule.Models
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("nombreCliente")]
        public string NombreCliente { get; set; }

        [Required]
        [BsonElement("apellidoUnoCliente")]
        public string ApellidoUnoCliente { get; set; }

        [BsonElement("apellidoDosCliente")]
        public string ApellidoDosCliente { get; set; }

        [Required]
        [BsonElement("cedulaCliente")]
        public string CedulaCliente { get; set; }

        [Required]
        [BsonElement("contactoCliente")]
        public ContactoCliente contactoCliente { get; set; }

        public class ContactoCliente
        {
            [Required]
            [BsonElement("numTelefonoUnoCliente")]
            public string NumTelefonoUnoCliente { get; set; }

            [BsonElement("numTelefonoDosCliente")]
            public string NumTelefonoDosCliente { get; set; }

            [Required]
            [BsonElement("correoCliente")]
            public string CorreoCliente { get; set; }
        }
    }
}