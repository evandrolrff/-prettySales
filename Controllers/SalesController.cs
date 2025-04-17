using AtividadeFinal.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace AtividadeFinal.Controllers
{
    public class SalesController : GenericDatabase<Sales>
    {
        public SalesController() : base() { }

        /// <summary>
        /// Adiciona no banco um <see cref="Sales"/>.
        /// </summary>
        /// <param name="classT"></param>
        /// <returns></returns>
        public override bool AddObject(Sales classT)
        {
            bool sucess = false;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Sales (userId, productId, quantity, saleDate) " +
                        "VALUES (@userId, @productId, @quantity, @saleDate)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", classT.User.Id);
                        cmd.Parameters.AddWithValue("@productId", classT.Product.Id);
                        cmd.Parameters.AddWithValue("@quantity", classT.Quantity);
                        cmd.Parameters.AddWithValue("@saleDate", classT.SaleDate);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                            sucess = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao inserir sales: " + ex.Message);
                }
            }

            return sucess;
        }

        /// <summary>
        /// Busca todos os <see cref="Sales"/> no banco de dados.
        /// </summary>
        /// <returns></returns>
        public override List<Sales> GetAllRegistry()
        {
            List<Sales> sales = new List<Sales>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    
                    using(SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Sales", connection))
                    {
                        using(SQLiteDataReader reader = cmd.ExecuteReader()){
                            while (reader.Read())
                            {
                                Sales sale = new Sales();
                                sales.Add(sale.FromReader(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao buscar um pagamento: " + ex.Message);
                }
            }
            return sales;
        }

        /// <summary>
        /// Retorna um <see cref="Sales"/> do banco de dados a partir de um id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Sales GetObjectById(int id)
        {
            Sales sale = new Sales();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    
                    using(SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Sales WHERE id = @Id", connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                sale.FromReader(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao buscar Sale: " + ex.Message);
                }
            }
            return sale;
        }

        /// <summary>
        /// Remove um <see cref="Sales"/> do banco.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override bool RemoveObject(int id)
        {
            bool sucess = false;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Sales WHERE id = @Id";
                    
                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                            sucess = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao excluir Sales: " + ex.Message);
                }
            }
            return sucess;
        }

        /// <summary>
        /// Atualiza no banco um <see cref="Sales"/> especifico.
        /// </summary>
        /// <param name="classT"></param>
        /// <returns></returns>
        public override bool UpdateObject(Sales classT)
        {
            bool sucess = false;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Sales SET userId = @userId, productId = @productId, " +
                        "quantity = @quantity, saleDate = @saleDate WHERE id = @Id";
                    
                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection)){
                        cmd.Parameters.AddWithValue("@Id", classT.Id);
                        cmd.Parameters.AddWithValue("@userId", classT.User.Id);
                        cmd.Parameters.AddWithValue("@productId", classT.Product.Id);
                        cmd.Parameters.AddWithValue("@quantity", classT.Quantity);
                        cmd.Parameters.AddWithValue("@saleDate", classT.SaleDate);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                            sucess = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao atualizar Sales: " + ex.Message);
                }
            }
            return sucess;
        }
    }
}
