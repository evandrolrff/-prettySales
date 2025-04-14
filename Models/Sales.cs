using System.Data.SQLite;
using System;
using AtividadeFinal.Controllers;

namespace AtividadeFinal.Models
{
    public class Sales
    {
        private int id;
        private User user;
        public string UserName;
        private Product product;
        public string ProductName;
        private int quantity;
        private DateTime saleDate;

        public Sales(int id, User user, Product product, int quantity, DateTime saleDate)
        {
            this.id = id;
            this.user = user;
            this.product = product;
            this.quantity = quantity;
            this.saleDate = saleDate;
        }

        public Sales() { }

        public int Id 
        { 
            get { return id; } 
            set { id = value; }
        }

        public User User 
        { 
            get { return user; }
            set 
            { 
                user = value;
                UserName = $"{user.Name} {user.LastName}";
            }
        }

        public Product Product 
        { 
            get { return product; }
            set 
            { 
                product = value;
                ProductName = product.Name;
            }
        }

        public int Quantity 
        { 
            get { return quantity; }
            set { quantity = value; }
        }

        public DateTime SaleDate 
        { 
            get { return saleDate; } 
            set { saleDate = value; }
        }

        public Sales FromReader(SQLiteDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("id"));
            Quantity = reader.GetInt32(reader.GetOrdinal("quanity"));
            SaleDate = reader.GetDateTime(reader.GetOrdinal("saleDate"));

            User = new UserController().GetObjectById(reader.GetInt32(reader.GetOrdinal("userId")));
            Product = new ProductController().GetObjectById(reader.GetInt32(reader.GetOrdinal("productId")));

            return this;
        }
    }
}
