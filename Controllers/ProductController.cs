using AtividadeFinal.Models;
using System.Data.SQLite;
using System;
using System.Collections.Generic;

namespace AtividadeFinal.Controllers
{
    public class ProductController
    {
        private readonly string connectionString;

        public ProductController(string connectionString)
        {
            this.connectionString = connectionString;
        }


        /// <summary>
        /// Busca todos os <see cref="Product"/> no banco de dados.
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Products", connection);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Product product = new Product();
                        products.Add(product.FromReader(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao buscar produtos: " + ex.Message);
                }
            }
            return products;
        }

        /// <summary>
        /// Retorna um <see cref="Product"/> do banco de dados a partir de um id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductById(int id)
        {
            Product product = new Product();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Products WHERE Id = @Id", connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        product.FromReader(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao buscar produto: " + ex.Message);
                }
            }
            return product;
        }

        /// <summary>
        /// Adiciona no banco um <see cref="Product"/>.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool AddProduct(Product product)
        {
            bool sucess = false;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Products (name, description, price, pathImage) " +
                        "VALUES (@name, @description, @price, @pathImage)";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", product.Name);
                    cmd.Parameters.AddWithValue("@description", product.Description);
                    cmd.Parameters.AddWithValue("@price", product.Price);
                    cmd.Parameters.AddWithValue("@pathImage", product.PathImage);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                        sucess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao inserir produto: " + ex.Message);
                }
            }

            return sucess;
        }

        /// <summary>
        /// Remove um <see cref="Product"/> do banco.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool RemoveProduct(int productId)
        {
            bool sucess = false;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Products WHERE Id = @Id";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", productId);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                        sucess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao excluir produto: " + ex.Message);
                }
            }
            return sucess;
        }

        /// <summary>
        /// Atualiza no banco um <see cref="Product"/> especifico.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool UpdateProduct(Product product)
        {
            bool sucess = false;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Products SET name = @name, description = @description, price = @price, " +
                        "pathImage = @pathImage WHERE Id = @Id";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", product.Id);
                    cmd.Parameters.AddWithValue("@name", product.Name);
                    cmd.Parameters.AddWithValue("@description", product.Description);
                    cmd.Parameters.AddWithValue("@price", product.Price);
                    cmd.Parameters.AddWithValue("@pathImage", product.PathImage);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                        sucess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao atualizar produto: " + ex.Message);
                }
            }
            return sucess;
        }
    }
}
