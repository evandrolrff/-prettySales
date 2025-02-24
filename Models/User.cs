using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
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
                this.name = value;
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                this.lastName = value;
            }
        }

        public string Address
        {
            get { return this.address; }
            set
            {
                this.address = value;
            }
        }

        public string Number
        { 
            get { return this.number; }
            set
            {
                this.number = value;
            }
        }

        public string Complement
        {
            get { return this.complement; }
            set
            {
                this.complement = value;
            }
        }

        public User FromReader(SQLiteDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("id"));
            Name = reader.GetString(reader.GetOrdinal("name"));
            LastName = reader.GetString(reader.GetOrdinal("LastName"));
            Address = reader.GetString(reader.GetOrdinal("address"));
            Number = reader.GetString(reader.GetOrdinal("number"));
            Complement = reader.GetString(reader.GetOrdinal("complement"));

            return this;
        }
    }
}
