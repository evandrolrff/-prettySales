using System.Collections.Generic;
using System.Configuration;

namespace AtividadeFinal.Controllers
{
    public abstract class GenericDatabase<T> where T : class
    {
        protected readonly string connectionString;

        protected GenericDatabase()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConnectionBD"].ConnectionString;
        }

        public abstract List<T> GetAllRegistry();

        public abstract T GetObjectById(int id);

        public abstract bool AddObject(T classT);

        public abstract bool RemoveObject(int id);

        public abstract bool UpdateObject(T classT);
    }
}
