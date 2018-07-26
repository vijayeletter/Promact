using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DAL
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DatabaseContext")
        {
            //Database.SetInitializer<DatabaseContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> DbEmployee { get; set; }
        public DbSet<Department> DbDepartment { get; set; }

        
    }
}
