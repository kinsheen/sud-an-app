using Microsoft.EntityFrameworkCore;
using Sudan_Api.Models;

namespace Sudan_Api.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }
        public DbSet<Sudan> Sudans { get; set; }
        public DbSet<SudanCategory> SudanCategories { get; set; }
    }
}
