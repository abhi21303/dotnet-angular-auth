using DotNetWebApi.Entitie;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetWebApi.Context
{
    public partial class TenantAppContext : IdentityDbContext<ApplicationUser>
    {
        public TenantAppContext(DbContextOptions<TenantAppContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply partial configurations
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }               
}
