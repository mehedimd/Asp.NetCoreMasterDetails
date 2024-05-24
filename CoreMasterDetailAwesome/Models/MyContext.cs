using Microsoft.EntityFrameworkCore;

namespace CoreMasterDetailAwesome.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext>options): base(options)
        {
            
        }
        public DbSet<Faculty> Faculties { get; set; } 
        public DbSet<Student> Students { get; set; }
    }
}
