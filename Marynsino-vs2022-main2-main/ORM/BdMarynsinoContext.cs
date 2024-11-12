using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Marynsino2513.ORM;

public partial class BdMarynsinoContext : DbContext
{
    public BdMarynsinoContext()
    {
    }

    public BdMarynsinoContext(DbContextOptions<BdMarynsinoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbAtendimento> TbAtendimentos { get; set; }

    public virtual DbSet<TbServico> TbServicos { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    public virtual DbSet<ViewMarynsino> ViewMarynsinos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAB205_11\\SQLEXPRESS;Database=BD_Marynsino;User Id=Leo_Marynsino;Password=marylinda1301;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbAtendimento>(entity =>
        {
            entity.ToTable("Tb_Atendimento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DtHoraAgendamento).HasColumnType("datetime");
            entity.Property(e => e.FkServico).HasColumnName("fk_servico");
            entity.Property(e => e.FkUsuarioId).HasColumnName("fk_Usuario_ID");

            entity.HasOne(d => d.FkServicoNavigation).WithMany(p => p.TbAtendimentos)
                .HasForeignKey(d => d.FkServico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_Atendimento_Tb_Servico");

            entity.HasOne(d => d.FkUsuario).WithMany(p => p.TbAtendimentos)
                .HasForeignKey(d => d.FkUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_Atendimento_Tb_Usuario");
        });

        modelBuilder.Entity<TbServico>(entity =>
        {
            entity.ToTable("Tb_Servico");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TipoServico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.ToTable("Tb_Usuario");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewMarynsino>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_Marynsino");

            entity.Property(e => e.DtHoraAgendamento).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoServico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
