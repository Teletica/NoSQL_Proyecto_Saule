using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NoSQL_Proyecto_Saule.Models
{
    public class Venta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("totalBrutoVenta")]
        public decimal TotalBrutoVenta { get; set; }

        [BsonElement("descuentoVenta")]
        public double DescuentoVenta { get; set; }

        [BsonElement("impuestoVenta")]
        public double ImpuestoVenta { get; set; }

        [BsonElement("totalNetoVenta")]
        public decimal TotalNetoVenta { get; set; }

        [BsonElement("idCompra")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdCompra { get; set; }
    }
}
