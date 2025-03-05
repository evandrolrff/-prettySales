using System.Data.SQLite;
using System;

namespace AtividadeFinal.Models
{
    public class User
    {
        private int id;
        private string name;
        private string lastName;
        private string address;
        private string number;
        private string complement;

        public User(int id, string name, string lastName, string address, string number, string complement = "")
        {
            this.id = id;
            this.name = name;
            this.lastName = lastName;
            this.address = address;
            this.number = number;
            this.complement = complement;
        }

        public User() { }

        public int Id 
        { 
            get { return this.id; }
            set
            {
                this.id = value;
            }
        }

        public string Name 
        { 
            get { return this.name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.name = value;
                }
                else
                {
                    this.name = "";
                }
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.lastName = value;
                }
                else
                {
                    this.lastName = "";
                }
            }
        }

        public string Address
        {
            get { return this.address; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.address = value;
                }
                else
                {
                    this.address = "";
                }
            }
        }

        public string Number
        { 
            get { return this.number; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.number = value;
                }
                else
                {
                    this.number = "";
                }
            }
        }

        public string Complement
        {
            get { return this.complement; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.complement = value;
                }
                else
                {
                    this.complement = "";
                }
            }
        }

        public User FromReader(SQLiteDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("id"));
            Name = reader.GetString(reader.GetOrdinal("name"));
            LastName = reader.GetString(reader.GetOrdinal("LastName"));
            Address = reader.GetString(reader.GetOrdinal("address"));
            Number = reader.GetString(reader.GetOrdinal("number"));
            try
            {
                Complement = reader.GetString(reader.GetOrdinal("complement"));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erro: {ex}");
                Complement = "";
            }

            return this;
        }
    }
}
