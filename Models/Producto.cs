using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoSQL_Proyecto_Saule.Models
{
    public class Producto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } // Corresponde al "_id" en MongoDB

        [Required]
        [BsonElement("nombreProducto")]
        public string NombreProducto { get; set; }

        [BsonElement("descripcionProducto")]
        public string DescripcionProducto { get; set; }

        [BsonElement("imagenProducto")]
        public string ImagenProducto { get; set; }

        [BsonElement("stockProducto")]
        public int StockProducto { get; set; }

        [BsonElement("idGrupo")]
        public IdContainer IdGrupo { get; set; }

        [BsonElement("idMarca")]
        public IdContainer IdMarca { get; set; }

        [BsonElement("idProveedor")]
        public IdContainer IdProveedor { get; set; }
    }

    public class IdContainer
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }

}