using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NoSQL_Proyecto_Saule.Models
{
    public class DbContex : DbContext
    {
        private readonly IMongoDatabase _database;
        public DbContex()
        {
            // Especificacion del puerto destinado al ambiente de produccion 
            var client = new MongoClient("mongodb://localhost:27020");

            // Especificar la coleccion generada 
            _database = client.GetDatabase("SauleFarmacia");
        }

        public IMongoCollection<Rol> RolCollection
        {
            get
            {
                return _database.GetCollection<Rol>("Rol");
            }
        }

        public IMongoCollection<Categoria>CategorialCollection
        {
            get
            {
                return _database.GetCollection<Categoria>("Categoria");
            }
        }
        public IMongoCollection<Empleado> EmpleadoCollection
        {
            get
            {
                return _database.GetCollection<Empleado>("Empleado");
            }
        }
        public IMongoCollection<Facturas> FacturasCollection
        {
            get
            {
                return _database.GetCollection<Facturas>("Facturas");
            }
        }
        public IMongoCollection<Cliente> ClienteCollection
        {
            get
            {
                return _database.GetCollection<Cliente>("Cliente");
            }
        }
        public IMongoCollection<Grupo> GrupoCollection
        {
            get
            {
                return _database.GetCollection<Grupo>("Grupo");
            }
        }
        public IMongoCollection<Proveedor> ProveedorCollection
        {
            get
            {
                return _database.GetCollection<Proveedor>("Proveedor");
            }
        }
        public IMongoCollection<Producto> ProductoCollection
        {
            get
            {
                return _database.GetCollection<Producto>("Producto");
            }
        }
        public IMongoCollection<Marca> MarcaCollection
        {
            get
            {
                return _database.GetCollection<Marca>("Marca");
            }
        }
        public IMongoCollection<Compras> ComprasCollection
        {
            get
            {
                return _database.GetCollection<Compras>("Compras");
            }
        }
        public IMongoCollection<Receta> RecetasCollection
        {
            get
            {
                return _database.GetCollection<Receta>("Receta");
            }
        }

        public System.Data.Entity.DbSet<NoSQL_Proyecto_Saule.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<NoSQL_Proyecto_Saule.Models.Marca> Marcas { get; set; }

        public System.Data.Entity.DbSet<NoSQL_Proyecto_Saule.Models.Proveedor> Proveedors { get; set; }

        public System.Data.Entity.DbSet<NoSQL_Proyecto_Saule.Models.Receta> Recetas { get; set; }

        public System.Data.Entity.DbSet<NoSQL_Proyecto_Saule.Models.Rol> Rols { get; set; }
    }
}
