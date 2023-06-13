using Microsoft.EntityFrameworkCore;
using MSTART_Task.ViewModels;

namespace MSTART_Task.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<MSTART_Task.ViewModels.AccountViewModel> AccountViewModel { get; set; } = default!;
    }
}
