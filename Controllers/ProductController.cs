using AtividadeFinal.Models;
using System.Data.SQLite;
using System;
using System.Collections.Generic;

namespace AtividadeFinal.Controllers
{
    public class ProductController : GenericDatabase<Product>
    {
        public ProductController() : base() { }

        /// <summary>
        /// Busca todos os <see cref="Product"/> no banco de dados.
        /// </summary>
        /// <returns></returns>
        public override List<Product> GetAllRegistry()
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
        public override Product GetObjectById(int id)
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
        public override bool AddObject(Product classT)
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
                    cmd.Parameters.AddWithValue("@name", classT.Name);
                    cmd.Parameters.AddWithValue("@description", classT.Description);
                    cmd.Parameters.AddWithValue("@price", classT.Price);
                    cmd.Parameters.AddWithValue("@pathImage", classT.PathImage);

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
        public override bool RemoveObject(int id)
        {
            bool sucess = false;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Products WHERE Id = @Id";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", id);

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
        public override bool UpdateObject(Product classT)
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
                    cmd.Parameters.AddWithValue("@Id", classT.Id);
                    cmd.Parameters.AddWithValue("@name", classT.Name);
                    cmd.Parameters.AddWithValue("@description", classT.Description);
                    cmd.Parameters.AddWithValue("@price", classT.Price);
                    cmd.Parameters.AddWithValue("@pathImage", classT.PathImage);

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
