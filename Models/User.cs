using MySql.Data.MySqlClient;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeFinal.Models
{
    public class User
    {
        private int id;
        private string name;
        private string lastName;
        private string address;
        private int number;
        private string complement;

        public User(int id, string name, string lastName, string address, int number, string complement = "")
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

        public int Number
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

        public User FromReader(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            Name = reader.GetString("name");
            LastName = reader.GetString("LastName");
            Address = reader.GetString("address");
            Number = reader.GetInt32("number");
            Complement = reader.GetString("complement");

            return this;
        }
    }
}
