using System.Data.Entity;
using Passificator.Model;

namespace Passificator.Data
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<Staff> Administrators { get; set; }
        public DatabaseContext() : base("sqlite")
        {
 
        }
    }
}
