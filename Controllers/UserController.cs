using AtividadeFinal.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace AtividadeFinal.Controllers
{
    public class UserController
    {
        private readonly string connectionString;

        public UserController(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users", connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

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

        public User GetUserById(int id)
        {
            User user = new User();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users WHERE Id = @Id", connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    MySqlDataReader reader = cmd.ExecuteReader();

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

        public bool AddUser(User user)
        {
            bool sucess= false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Users (name, lastName, address, number, complement) " +
                        "VALUES (@name, @lastName, @address, @number, @complement)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@lastName", user.LastName);
                    cmd.Parameters.AddWithValue("@address", user.Address);
                    cmd.Parameters.AddWithValue("@number", user.Number);
                    cmd.Parameters.AddWithValue("@complement", user.Complement);

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

        public bool RemoveUser(int userId)
        {
            bool sucess= false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Users WHERE Id = @Id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", userId);

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

        public bool UpdateUser(User user)
        {
            bool sucess = false;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Users SET name = @name, lastName = @lastName, address = @address, " +
                        "number = @number, complement = @complement WHERE Id = @Id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@lastName", user.LastName);
                    cmd.Parameters.AddWithValue("@address", user.Address);
                    cmd.Parameters.AddWithValue("@number", user.Number);
                    cmd.Parameters.AddWithValue("@complement", user.Complement);

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
