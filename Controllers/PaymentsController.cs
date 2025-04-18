using AtividadeFinal.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace AtividadeFinal.Controllers
{
    public class PaymentsController : GenericDatabase<Payments>
    {
        public PaymentsController() : base() { }

        /// <summary>
        /// Adiciona no banco um <see cref="Payments"/>.
        /// </summary>
        /// <param name="classT"></param>
        /// <returns></returns>
        public override bool AddObject(Payments classT)
        {
            bool sucess = false;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Payments (userId, saleId, amount, paymentDate) " +
                        "VALUES (@userId, @saleId, @amount, @paymentDate)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", classT.User.Id);
                        cmd.Parameters.AddWithValue("@saleId", classT.Sale.Id);
                        cmd.Parameters.AddWithValue("@amount", classT.Amount);
                        cmd.Parameters.AddWithValue("@paymentDate", classT.PaymentDate);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                            sucess = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao inserir payments: " + ex.Message);
                }
            }

            return sucess;
        }

        /// <summary>
        /// Busca todos os <see cref="Payments"/> no banco de dados.
        /// </summary>
        /// <returns></returns>
        public override List<Payments> GetAllRegistry()
        {
            List<Payments> payments = new List<Payments>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    
                    using(SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Payments", connection))
                    {
                        using(SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Payments payment = new Payments();
                                payments.Add(payment.FromReader(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao buscar um pagamento: " + ex.Message);
                }
            }
            return payments;
        }

        /// <summary>
        /// Retorna uma lista de <see cref="Sales"/> do banco de dados
        /// a partir de um <see cref="User"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Sales> GetUserSales(User user)
        {
            List<Sales> sales = new List<Sales>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT id, userId, productId, quantity, saleDate "+
                            "FROM Sales WHERE userId = @userId";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", user.Id);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
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
                    Console.WriteLine("Erro ao buscar compras do usuário: " + ex.Message);
                }
            }

            return sales;
        }

        /// <summary>
        /// Retorna um <see cref="Payments"/> do banco de dados a partir de um id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Payments GetObjectById(int id)
        {
            Payments payment = new Payments();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Payments WHERE id = @Id", connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                    
                        using(SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                payment.FromReader(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao buscar payment: " + ex.Message);
                }
            }
            return payment;
        }

        /// <summary>
        /// Remove um <see cref="Payments"/> do banco.
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
                    string query = "DELETE FROM Payments WHERE id = @Id";

                    using(SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                            sucess = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao excluir payment: " + ex.Message);
                }
            }
            return sucess;
        }

        /// <summary>
        /// Atualiza no banco um <see cref="Payments"/> especifico.
        /// </summary>
        /// <param name="classT"></param>
        /// <returns></returns>
        public override bool UpdateObject(Payments classT)
        {
            bool sucess = false;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Payments SET userId = @userId, saleId = @saleId, amount = @amount, " +
                        "paymentDate = @paymentDate WHERE id = @Id";
                    
                    using(SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", classT.Id);
                        cmd.Parameters.AddWithValue("@userId", classT.User.Id);
                        cmd.Parameters.AddWithValue("@saleId", classT.Sale.Id);
                        cmd.Parameters.AddWithValue("@amount", classT.Amount);
                        cmd.Parameters.AddWithValue("@paymentDate", classT.PaymentDate);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                            sucess = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao atualizar Payments: " + ex.Message);
                }
            }
            return sucess;
        }
    }
}
