using System;
using System.Data.SQLite;

namespace AtividadeFinal.Models
{
    public class Payments
    {
        private int id;
        private User user;
        private Sales sale;
        private int amount;
        private DateTime paymentDate;

        public Payments(int id, User user, Sales sale, int amount, DateTime paymentDate) 
        {
            this.id = id;
            this.user = user;
            this.sale = sale;
            this.amount = amount;
            this.paymentDate = paymentDate;
        }

        public Payments() { }

        public int Id { get { return id; } set { id = value; } }

        public User User { get { return user; }  set { user = value; } }

        public Sales Sale { get { return sale; } set { sale = value; } }

        public int Amount { get { return amount; } set { amount = value; } }

        public DateTime PaymentDate { get { return paymentDate; } set { paymentDate = value; } }

        private static string ConvertTimeStampToString(DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public Payments FromReader(SQLiteDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("id"));
            //User = reader.GetInt32(reader.GetOrdinal("userId"));
            //Sale = reader.GetInt32(reader.GetOrdinal("saleId")); => PODE SER NULO
            Amount = reader.GetInt32(reader.GetOrdinal("amount"));
            PaymentDate = reader.GetDateTime(reader.GetOrdinal("paymentDate"));

            return this;
        }
    }
}