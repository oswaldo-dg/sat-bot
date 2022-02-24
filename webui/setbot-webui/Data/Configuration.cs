using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using satbot_infrastructure.models;

namespace setbot_webui.Data
{
    public class DbConfUsuarioRFC : IEntityTypeConfiguration<UsuarioRFC>
    {
        public void Configure(EntityTypeBuilder<UsuarioRFC> builder)
        {
            builder.ToTable("UsuarioRFC");
            builder.HasKey(x => new { x.UsuarioId, x.RFC });

            builder.Property(x => x.UsuarioId).HasMaxLength(450).IsRequired();
            builder.Property(x => x.RFC).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Denominacion).IsRequired();
            builder.Property(x => x.MaximoCFDIS).IsRequired();
            builder.Property(x => x.CertificadoValidoHasta).IsRequired(false);
            builder.Property(x => x.EnEvaluacion).IsRequired();
            builder.Property(x => x.FinEvaluacion).IsRequired(false);
            builder.Property(x => x.InicioEvaluacion).IsRequired(false);
            builder.Property(x => x.SecretoId).HasMaxLength(100).IsRequired(false);
            builder.Property(x => x.Validado).IsRequired();
            builder.HasOne(x => x.Usuario).WithMany(y => y.UsuariosRFC).HasForeignKey(z => z.UsuarioId).OnDelete(DeleteBehavior.Cascade);
        }
    }



    public class DbConfConfiguracionExtraccionRFC : IEntityTypeConfiguration<ConfiguracionExtraccionRFC>
    {
        public void Configure(EntityTypeBuilder<ConfiguracionExtraccionRFC> builder)
        {
            builder.ToTable("ConfiguracionExtraccionRFC");
            builder.HasKey(x => new { x.UsuarioId, x.RFC });

            builder.Property(x => x.UsuarioId).HasMaxLength(450).IsRequired();
            builder.Property(x => x.RFC).HasMaxLength(20).IsRequired();
            builder.Property(x => x.FechaInicial).IsRequired();
            builder.Property(x => x.ExtraerMetadatosEmisor).IsRequired();
            builder.Property(x => x.ExtraerMetadatosReceptor).IsRequired();
            builder.Property(x => x.ExtraerXMLEmisor).IsRequired();
            builder.Property(x => x.ExtraerXMLReceptor).IsRequired();
            builder.Property(x => x.ExtraerPDFEmisor).IsRequired();
            builder.Property(x => x.ExtraerPDFReceptor).IsRequired();
            builder.Property(x => x.ExtraerCanceladosEmisor).IsRequired();
            builder.Property(x => x.ExtraerCanceladosReceptor).IsRequired();
            builder.Property(x => x.Habilitada).IsRequired();

            builder.HasOne(x => x.Usuario).WithMany(y => y.ConfiguracionesExtraccionRFC).HasForeignKey(z => z.UsuarioId).OnDelete(DeleteBehavior.Cascade);
        }
    }



    public class DbConfPluginRFC : IEntityTypeConfiguration<PluginRFC>
    {
        public void Configure(EntityTypeBuilder<PluginRFC> builder)
        {
            builder.ToTable("PluginRFC");
            builder.HasKey(x => new { x.UsuarioId, x.RFC, x.PluginId });

            builder.Property(x => x.UsuarioId).HasMaxLength(450).IsRequired();
            builder.Property(x => x.RFC).HasMaxLength(20).IsRequired();
            builder.Property(x => x.PluginId).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Activo).IsRequired();

            builder.HasOne(x => x.Usuario).WithMany(y => y.PluginsRFC).HasForeignKey(z => z.UsuarioId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
