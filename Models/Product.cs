using System.Data.SQLite;
using System;

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

        public int Id 
        { 
            get { return id; }
            set { id = value; } 
        }
        public string Name 
        { 
            get { return name; }
            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    name = value;
                }
                else
                {
                    name = "";
                }
            }
        }
        public string Description 
        { 
            get { return description; }
            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    description = value;
                }
                else
                {
                    description = "";
                }
            } 
        }
        public float Price 
        { 
            get { return price; }
            set { price = value; } 
        }
        public string PathImage 
        { 
            get { return pathImage; }
            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    pathImage = value;
                }
                else
                {
                    PathImage = "";
                }
            } 
        }

        public Product FromReader(SQLiteDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("id"));
            Name = reader.GetString(reader.GetOrdinal("name"));
            Description = reader.GetString(reader.GetOrdinal("description"));
            Price = reader.GetFloat(reader.GetOrdinal("price"));
            try
            {
                PathImage = reader.GetString(reader.GetOrdinal("pathImage"));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erro: {ex}");
                PathImage = "";
            }

            return this;
        }
    }
}
