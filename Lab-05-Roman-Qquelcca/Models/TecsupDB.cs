using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Lab_05_Roman_Qquelcca.Models;

public partial class TecsupDB : DbContext
{
    public TecsupDB()
    {
    }

    public TecsupDB(DbContextOptions<TecsupDB> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencia> Asistencias { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Evaluacione> Evaluaciones { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=tecsup;uid=root;pwd=123456;port=3306", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("PRIMARY");

            entity.ToTable("asistencias");

            entity.HasIndex(e => e.IdCurso, "id_curso");

            entity.HasIndex(e => e.IdEstudiante, "id_estudiante");

            entity.Property(e => e.IdAsistencia).HasColumnName("id_asistencia");
            entity.Property(e => e.Estado)
                .HasColumnType("enum('Presente','Ausente','Justificada')")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdCurso)
                .HasConstraintName("asistencias_ibfk_2");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdEstudiante)
                .HasConstraintName("asistencias_ibfk_1");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PRIMARY");

            entity.ToTable("cursos");

            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.Creditos).HasColumnName("creditos");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PRIMARY");

            entity.ToTable("estudiantes");

            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Evaluacione>(entity =>
        {
            entity.HasKey(e => e.IdEvaluacion).HasName("PRIMARY");

            entity.ToTable("evaluaciones");

            entity.HasIndex(e => e.IdCurso, "id_curso");

            entity.HasIndex(e => e.IdEstudiante, "id_estudiante");

            entity.Property(e => e.IdEvaluacion).HasColumnName("id_evaluacion");
            entity.Property(e => e.Calificacion)
                .HasPrecision(5, 2)
                .HasColumnName("calificacion");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Evaluaciones)
                .HasForeignKey(d => d.IdCurso)
                .HasConstraintName("evaluaciones_ibfk_2");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Evaluaciones)
                .HasForeignKey(d => d.IdEstudiante)
                .HasConstraintName("evaluaciones_ibfk_1");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PRIMARY");

            entity.ToTable("materias");

            entity.HasIndex(e => e.IdCurso, "id_curso");

            entity.Property(e => e.IdMateria).HasColumnName("id_materia");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Materia)
                .HasForeignKey(d => d.IdCurso)
                .HasConstraintName("materias_ibfk_1");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.IdMatricula).HasName("PRIMARY");

            entity.ToTable("matriculas");

            entity.HasIndex(e => e.IdCurso, "id_curso");

            entity.HasIndex(e => e.IdEstudiante, "id_estudiante");

            entity.Property(e => e.IdMatricula).HasColumnName("id_matricula");
            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.Semestre)
                .HasMaxLength(20)
                .HasColumnName("semestre");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.IdCurso)
                .HasConstraintName("matriculas_ibfk_2");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.IdEstudiante)
                .HasConstraintName("matriculas_ibfk_1");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.IdProfesor).HasName("PRIMARY");

            entity.ToTable("profesores");

            entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Especialidad)
                .HasMaxLength(100)
                .HasColumnName("especialidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
