using System.Data.Entity;
using DataLayer.Entities;

namespace DataLayer.Concrete
{
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base("DBPacManRecords")
        {
        }

        public DbSet<Record> Records { get; set; }
    }
}
