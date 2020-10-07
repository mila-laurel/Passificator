using System.Data.Common;
using System.Data.Entity;
using Passificator.Model;

namespace Passificator.Data
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<Staff> Administrators { get; set; }
        public DatabaseContext() : base(GetConnection(), false)
        {
 
        }

        private static DbConnection GetConnection()
        {
            const string providerName = "System.Data.SQLite.EF6";
            const string connectionString = "Data Source=.\\Data\\Creatures.db"; 
            var factory = DbProviderFactories.GetFactory(providerName);
            var dbCon = factory.CreateConnection();
            dbCon.ConnectionString = connectionString;
            return dbCon;
        }
    }
}
