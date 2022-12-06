using Microsoft.EntityFrameworkCore;
using webdoan.Areas.Admin.Models;

namespace webdoan.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
           
        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<AdminMenu> AdminMenus { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<webdoan.Areas.Admin.Models.Account> Account { get; set; }
        public DbSet<webdoan.Areas.Admin.Models.Role> Role { get; set; }
        public DbSet<webdoan.Areas.Admin.Models.Categories> Categories { get; set; }
    }
}
