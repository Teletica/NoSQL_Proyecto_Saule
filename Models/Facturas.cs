using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoSQL_Proyecto_Saule.Models
{
    public class Facturas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("idCompra")]
        public BsonValue IdCompra { get; set; }

        [BsonElement("idVenta")]
        public BsonValue IdVenta { get; set; }
    }
}