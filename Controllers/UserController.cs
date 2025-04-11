using System.Collections.Generic;
using AtividadeFinal.Models;
using System.Data.SQLite;
using System.Linq;
using System;


namespace AtividadeFinal.Controllers
{
    public class UserController : GenericDatabase<User>
    {
        public UserController() : base() { }

        /// <summary>
        /// Busca todos os <see cref="User"/> no banco de dados.
        /// </summary>
        /// <returns></returns>
        public override List<User> GetAllRegistry()
        {
            List<User> users = new List<User>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Users", connection);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        users.Add(user.FromReader(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao buscar usuários: " + ex.Message);
                }
            }
            return users;
        }

        /// <summary>
        /// Retorna um <see cref="User"/> do banco de dados a partir de um id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override User GetObjectById(int id)
        {
            User user = new User();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Users WHERE id = @Id", connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user.FromReader(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao buscar usuário: " + ex.Message);
                }
            }
            return user;
        }

        /// <summary>
        /// Adiciona no banco um <see cref="User"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override bool AddObject(User classT)
        {
            bool sucess = false;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Users (name, lastName, address, number, complement) " +
                        "VALUES (@name, @lastName, @address, @number, @complement)";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", classT.Name);
                    cmd.Parameters.AddWithValue("@lastName", classT.LastName);
                    cmd.Parameters.AddWithValue("@address", classT.Address);
                    cmd.Parameters.AddWithValue("@number", classT.Number);
                    cmd.Parameters.AddWithValue("@complement", classT.Complement);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                        sucess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao inserir usuário: " + ex.Message);
                }
            }

            return sucess;
        }

        /// <summary>
        /// Remove um <see cref="User"/> do banco.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public override bool RemoveObject(int id)
        {
            bool sucess = false;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Users WHERE id = @Id";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", id);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                        sucess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao excluir usuário: " + ex.Message);
                }
            }
            return sucess;
        }

        /// <summary>
        /// Atualiza no banco um <see cref="User"/> especifico.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override bool UpdateObject(User classT)
        {
            bool sucess = false;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Users SET name = @name, lastName = @lastName, address = @address, " +
                        "number = @number, complement = @complement WHERE id = @Id";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", classT.Id);
                    cmd.Parameters.AddWithValue("@name", classT.Name);
                    cmd.Parameters.AddWithValue("@lastName", classT.LastName);
                    cmd.Parameters.AddWithValue("@address", classT.Address);
                    cmd.Parameters.AddWithValue("@number", classT.Number);
                    cmd.Parameters.AddWithValue("@complement", classT.Complement);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                        sucess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao atualizar usuário: " + ex.Message);
                }
            }
            return sucess;
        }
    }
}
