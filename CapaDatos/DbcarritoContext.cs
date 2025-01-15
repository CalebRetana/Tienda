using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CapaEntidades.CapaEntidades;
using Microsoft.Data.SqlClient;
namespace CapaDatos;

public partial class DbcarritoContext : DbContext
{
    public DbcarritoContext()
    {
    }

    public DbcarritoContext(DbContextOptions<DbcarritoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<Distrito> Distritos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=CALEC;Database=DBCARRITO;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.IdCarrito).HasName("PK__CARRITO__83A2AD9CC8813B02");

            entity.ToTable("CARRITO");

            entity.Property(e => e.IdCarrito).HasColumnName("id_carrito");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_Carrito_Categoria");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_Carrito_Marca");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__CD54BC5A4B17ED63");

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__CLIENTE__677F38F5FAEEAFD5");

            entity.ToTable("CLIENTE");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Reestablecer)
                .HasDefaultValue(false)
                .HasColumnName("reestablecer");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Departamento");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdDepartamento)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id_departamento");
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DETALLE___3C2E445C1E573307");

            entity.ToTable("DETALLE_VENTA");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("id_detalleVenta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("fk_id_producto");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("fk_id_venta");
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Distrito");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdDepartamento)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id_departamento");
            entity.Property(e => e.IdDistrito)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("id_distrito");
            entity.Property(e => e.IdMunicipio)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("id_municipio");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__MARCA__7E43E99E4AC648D5");

            entity.ToTable("MARCA");

            entity.Property(e => e.IdMarca).HasColumnName("id_marca");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Municipio");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdDepartamento)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("id_departamento");
            entity.Property(e => e.IdMunicipio)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("id_municipio");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__PRODUCTO__FF341C0D1243BBD6");

            entity.ToTable("PRODUCTO");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdMarca).HasColumnName("id_marca");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreImagen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_imagen");
            entity.Property(e => e.Precio)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("precio");
            entity.Property(e => e.RutaImagen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ruta_imagen");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_Productos_Categoria");

            entity.HasOne(d => d.Marca).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK_Productos_Marca");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__8E901EAA4E322349");

            entity.ToTable("USUARIO");

            entity.Property(e => e.IdUsuario).HasColumnName("id_Usuario");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Clave)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Reestablecer)
                .HasDefaultValue(true)
                .HasColumnName("reestablecer");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__VENTA__459533BF4CE4F81F");

            entity.ToTable("VENTA");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Contacto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdDistrito)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id_distrito");
            entity.Property(e => e.IdTransaccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id_transaccion");
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("fk_id_cliente");
        });

        OnModelCreatingPartial(modelBuilder);
    }
   

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
