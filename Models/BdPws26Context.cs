using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PWS26ApiServer.Models;

public partial class BdPws26Context : DbContext
{
    public BdPws26Context()
    {
    }

    public BdPws26Context(DbContextOptions<BdPws26Context> options)
        : base(options)
    {
    }

    public virtual DbSet<TbComentario> TbComentarios { get; set; }

    public virtual DbSet<TbDepartamento> TbDepartamentos { get; set; }

    public virtual DbSet<TbEmpleado> TbEmpleados { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbComentario>(entity =>
        {
            entity.HasKey(e => e.IdComentario);

            entity.ToTable("tbComentarios");

            entity.Property(e => e.IdComentario).HasColumnName("idComentario");
            entity.Property(e => e.Comentarios).HasColumnName("comentarios");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoComentario)
                .HasMaxLength(50)
                .HasColumnName("tipoComentario");
        });

        modelBuilder.Entity<TbDepartamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__tbDepart__787A433DCE13C581");

            entity.ToTable("tbDepartamento");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<TbEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__tbEmplea__CE6D8B9E339A3A86");

            entity.ToTable("tbEmpleado");

            entity.Property(e => e.NombreCompleto).HasMaxLength(50);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.TbEmpleados)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbEmplead__IdDep__5EBF139D");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__tbUsuari__645723A682C8420E");

            entity.ToTable("tbUsuarios");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Pass)
                .HasMaxLength(100)
                .HasColumnName("pass");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
