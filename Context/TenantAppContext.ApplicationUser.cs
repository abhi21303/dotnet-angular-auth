using DotNetWebApi.Entitie;
using Microsoft.EntityFrameworkCore;

namespace DotNetWebApi.Context
{
    public partial class TenantAppContext
    {
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("AspNetUsers");
            });
        }
    }
}
