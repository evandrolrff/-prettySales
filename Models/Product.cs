using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeFinal.Models
{
    public class Product
    {
        private int id;
        private string name;
        private string description;
        private float price;
        private string pathImage;

        public Product(int id, string name, string description, float price, string pathImage)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.description = description;
            this.pathImage = pathImage;
        }

        public Product() { }

        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return description; } set { description = value; } }
        public float Price { get { return price; } set { price = value; } }
        public string PathImage { get { return pathImage; } set { pathImage = value; } }

        public Product FromReader(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            Name = reader.GetString("name");
            Description = reader.GetString("description");
            Price = reader.GetFloat("price");
            PathImage = reader.GetString("pathImage");

            return this;
        }
    }
}
