using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoSQL_Proyecto_Saule.Models
{
    public class Empleado
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("nombreEmpleado")]
        public string NombreEmpleado { get; set; }

        [Required]
        [BsonElement("apellidoUnoEmpleado")]
        public string ApellidoUnoEmpleado { get; set; }

        [BsonElement("apellidoDosEmpleado")]
        public string ApellidoDosEmpleado { get; set; }

        [Required]
        [BsonElement("salarioEmpleado")]
        public int SalarioEmpleado { get; set; }

        [BsonElement("descripcionHorarioEmpleado")]
        public string DescripcionHorarioEmpleado { get; set; }

        [BsonElement("idRol")]
        public IdContainer IdRol { get; set; }

        [Required]
        [BsonElement("contactoEmpleado")]
        public ContactoEmpleado contactoEmpelado { get; set; }

        public class IdContainer
        {
            [BsonElement("_id")]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }
        }

        public class ContactoEmpleado
        {
            [Required]
            [BsonElement("numTelefonoUnoEmpleado")]
            public string NumTelefonoUnoEmpleado { get; set; }

            [BsonElement("numTelefonoDosEmpleado")]
            public string NumTelefonoDosEmpleado { get; set; }

            [Required]
            [BsonElement("correoEmpleado")]
            public string CorreoEmpleado { get; set; }
        }


    }
}