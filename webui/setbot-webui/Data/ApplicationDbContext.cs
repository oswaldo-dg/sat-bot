using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using satbot_infrastructure.models;

namespace setbot_webui.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UsuarioRFC> UsuarisRFC { get; set; }
        public DbSet<PluginRFC> PluginRFC { get; set; }
        public DbSet<ConfiguracionExtraccionRFC> ConfiguracionExtraccionRFC { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new DbConfUsuarioRFC());
            builder.ApplyConfiguration(new DbConfConfiguracionExtraccionRFC());
            builder.ApplyConfiguration(new DbConfPluginRFC());

        }
    }
}