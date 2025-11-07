using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace proyecto_TBD.Models;

public partial class MydbContext : DbContext
{
    public MydbContext()
    {
    }

    public MydbContext(DbContextOptions<MydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Donativo> Donativos { get; set; }

    public virtual DbSet<Institucione> Instituciones { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=localhost;database=mydb;uid=root;pwd=admin", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Donativo>(entity =>
        {
            entity.HasKey(e => e.Iddonativos).HasName("PRIMARY");

            entity.ToTable("donativos");

            entity.HasIndex(e => e.IdInstituto, "idinstituciones_idx");

            entity.HasIndex(e => e.IdProducto, "idproductos_idx");

            entity.HasIndex(e => e.IdUsuario, "idusuario_idx");

            entity.Property(e => e.Iddonativos).HasColumnName("iddonativos");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdInstituto).HasColumnName("id_instituto");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdInstitutoNavigation).WithMany(p => p.Donativos)
                .HasForeignKey(d => d.IdInstituto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("idinstituciones");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Donativos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("idproductos");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Donativos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("idusuario");
        });

        modelBuilder.Entity<Institucione>(entity =>
        {
            entity.HasKey(e => e.IdInstituto).HasName("PRIMARY");

            entity.ToTable("instituciones");

            entity.Property(e => e.IdInstituto).HasColumnName("id_instituto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(150)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(45)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PRIMARY");

            entity.ToTable("productos");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Descripción)
                .HasMaxLength(255)
                .HasColumnName("descripción");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.PesoAprox)
                .HasPrecision(8, 2)
                .HasColumnName("peso_Aprox");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Correo, "correo_UNIQUE").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Correo)
                .HasMaxLength(200)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(150)
                .HasColumnName("nombre_empresa");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
