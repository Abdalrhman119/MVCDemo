using System.Reflection;

namespace Demo.DataAccess.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("ConnectionString");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        public DbSet<Department> Departments { get; set; }
    }
}
