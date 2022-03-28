using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace gestionPagos.Models
{
    public partial class pagosContext : DbContext
    {
        public pagosContext()
        {
        }

        public pagosContext(DbContextOptions<pagosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asignacion> Asignacions { get; set; }
        public virtual DbSet<Contacto> Contactos { get; set; }
        public virtual DbSet<Envio> Envios { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<TipoContacto> TipoContactos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:PagosDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Asignacion>(entity =>
            {
                entity.ToTable("asignacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Costo)
                    .HasColumnType("smallmoney")
                    .HasColumnName("costo");

                entity.Property(e => e.IdPedido).HasColumnName("id_pedido");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.Asignacions)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_asignacion_pedido");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Asignacions)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_asignacion_producto");
            });

            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => e.Documento);

                entity.ToTable("contacto");

                entity.Property(e => e.Documento)
                    .ValueGeneratedNever()
                    .HasColumnName("documento");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.NombreCompleto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_completo");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.Tipo).HasColumnName("tipo");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usuario");

                entity.HasOne(d => d.TipoNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.Tipo)
                    .HasConstraintName("FK_contacto_contacto");
            });

            modelBuilder.Entity<Envio>(entity =>
            {
                entity.ToTable("envio");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FechaEntrega)
                    .HasColumnType("date")
                    .HasColumnName("fechaEntrega");

                entity.Property(e => e.FechaEstimada)
                    .HasColumnType("date")
                    .HasColumnName("fechaEstimada");

                entity.Property(e => e.Guia)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("guia");

                entity.Property(e => e.IdPedido).HasColumnName("id_pedido");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.Envios)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK_envio_pedido");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("estado");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.Consecutivo);

                entity.ToTable("factura");

                entity.Property(e => e.Consecutivo).HasColumnName("consecutivo");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdPedido).HasColumnName("id_pedido");

                entity.Property(e => e.Total)
                    .HasColumnType("smallmoney")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK_factura_pedido");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("pedido");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK_pedido_estado");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.PedidoIdProveedorNavigations)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pedido_contacto1");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.PedidoIdUsuarioNavigations)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pedido_contacto");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("producto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.Valor)
                    .HasColumnType("smallmoney")
                    .HasColumnName("valor");
            });

            modelBuilder.Entity<TipoContacto>(entity =>
            {
                entity.ToTable("tipoContacto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
