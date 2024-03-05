using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Zlatara.Models;

namespace Zlatara.Data;

public partial class ZlataraContext : DbContext
{
    public ZlataraContext()
    {
    }

    public ZlataraContext(DbContextOptions<ZlataraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artikal> Artikals { get; set; }

    public virtual DbSet<Brend> Brends { get; set; }

    public virtual DbSet<Kamenje> Kamenjes { get; set; }

    public virtual DbSet<Kategorija> Kategorijas { get; set; }

    public virtual DbSet<Menadzer> Menadzers { get; set; }

    public virtual DbSet<OtkupljenArtikal> OtkupljenArtikals { get; set; }

    public virtual DbSet<Racun> Racuns { get; set; }

    public virtual DbSet<Radnik> Radniks { get; set; }

    public virtual DbSet<StavkaRacuna> StavkaRacunas { get; set; }

    public virtual DbSet<StorniranRacun> StorniranRacuns { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Zlatara;Trusted_Connection=True;Trust Server Certificate=True;Encrypt=True;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artikal>(entity =>
        {
            entity.ToTable("Artikal");

            entity.Property(e => e.ArtikalId)
                .ValueGeneratedNever()
                .HasColumnName("artikalID");
            entity.Property(e => e.BrendId).HasColumnName("brendID");
            entity.Property(e => e.Cena).HasColumnName("cena");
            entity.Property(e => e.Gramaza).HasColumnName("gramaza");
            entity.Property(e => e.KamenId).HasColumnName("kamenID");
            entity.Property(e => e.KategorijaId).HasColumnName("kategorijaID");
            entity.Property(e => e.KolicinaNaStanju).HasColumnName("kolicinaNaStanju");
            entity.Property(e => e.Materijal)
                .HasMaxLength(50)
                .HasColumnName("materijal");
            entity.Property(e => e.NazivArtikla)
                .HasMaxLength(50)
                .HasColumnName("nazivArtikla");
            entity.Property(e => e.Opis)
                .HasMaxLength(300)
                .HasColumnName("opis");
            entity.Property(e => e.Slika)
                .HasMaxLength(1000)
                .HasColumnName("slika");

            entity.HasOne(d => d.Brend).WithMany(p => p.Artikals)
                .HasForeignKey(d => d.BrendId)
                .HasConstraintName("FK_Artikal_Brend");

            entity.HasOne(d => d.Kamen).WithMany(p => p.Artikals)
                .HasForeignKey(d => d.KamenId)
                .HasConstraintName("FK_Artikal_Kamenje");

            entity.HasOne(d => d.Kategorija).WithMany(p => p.Artikals)
                .HasForeignKey(d => d.KategorijaId)
                .HasConstraintName("FK_Artikal_Kategorija");
        });

        modelBuilder.Entity<Brend>(entity =>
        {
            entity.ToTable("Brend");

            entity.Property(e => e.BrendId)
                .ValueGeneratedNever()
                .HasColumnName("brendID");
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .HasColumnName("naziv");
        });

        modelBuilder.Entity<Kamenje>(entity =>
        {
            entity.HasKey(e => e.KamenId);

            entity.ToTable("Kamenje");

            entity.Property(e => e.KamenId)
                .ValueGeneratedNever()
                .HasColumnName("kamenID");
            entity.Property(e => e.Boja)
                .HasMaxLength(50)
                .HasColumnName("boja");
            entity.Property(e => e.Cistoca).HasColumnName("cistoca");
            entity.Property(e => e.Karataz).HasColumnName("karataz");
            entity.Property(e => e.Kolcina).HasColumnName("kolcina");
            entity.Property(e => e.VrstaKamena)
                .HasMaxLength(50)
                .HasColumnName("vrstaKamena");
        });

        modelBuilder.Entity<Kategorija>(entity =>
        {
            entity.ToTable("Kategorija");

            entity.Property(e => e.KategorijaId)
                .ValueGeneratedNever()
                .HasColumnName("kategorijaID");
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .HasColumnName("naziv");
        });

        modelBuilder.Entity<Menadzer>(entity =>
        {
            entity.HasKey(e => e.User);

            entity.ToTable("Menadzer");

            entity.Property(e => e.User)
                .ValueGeneratedNever()
                .HasColumnName("user");
            entity.Property(e => e.Ime)
                .HasMaxLength(50)
                .HasColumnName("ime");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Prezime)
                .HasMaxLength(50)
                .HasColumnName("prezime");
        });

        modelBuilder.Entity<OtkupljenArtikal>(entity =>
        {
            entity.ToTable("OtkupljenArtikal");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.DatumOtkupa).HasColumnName("datumOtkupa");
            entity.Property(e => e.Finoca)
                .HasMaxLength(50)
                .HasColumnName("finoca");
            entity.Property(e => e.Gramaza).HasColumnName("gramaza");
            entity.Property(e => e.Materijal)
                .HasMaxLength(50)
                .HasColumnName("materijal");
            entity.Property(e => e.RadnikUser).HasColumnName("radnikUser");
            entity.Property(e => e.UkCenaOtkupa).HasColumnName("ukCenaOtkupa");

            entity.HasOne(d => d.RadnikUserNavigation).WithMany(p => p.OtkupljenArtikals)
                .HasForeignKey(d => d.RadnikUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OtkupljenArtikal_Radnik");
        });

        modelBuilder.Entity<Racun>(entity =>
        {
            entity.ToTable("Racun");

            entity.Property(e => e.RacunId)
                .ValueGeneratedNever()
                .HasColumnName("racunID");
            entity.Property(e => e.Cena).HasColumnName("cena");
            entity.Property(e => e.Datum).HasColumnName("datum");
            entity.Property(e => e.Pib).HasColumnName("PIB");
            entity.Property(e => e.RadnikUser).HasColumnName("radnikUser");

            entity.HasOne(d => d.RadnikUserNavigation).WithMany(p => p.Racuns)
                .HasForeignKey(d => d.RadnikUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Racun_Radnik");
        });

        modelBuilder.Entity<Radnik>(entity =>
        {
            entity.HasKey(e => e.User);

            entity.ToTable("Radnik");

            entity.Property(e => e.User)
                .ValueGeneratedNever()
                .HasColumnName("user");
            entity.Property(e => e.Ime)
                .HasMaxLength(50)
                .HasColumnName("ime");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Prezime)
                .HasMaxLength(50)
                .HasColumnName("prezime");
        });

        modelBuilder.Entity<StavkaRacuna>(entity =>
        {
            entity.HasKey(e => new { e.RedinBroj, e.RacunId });

            entity.ToTable("StavkaRacuna");

            entity.Property(e => e.RedinBroj).HasColumnName("redinBroj");
            entity.Property(e => e.RacunId).HasColumnName("racunID");
            entity.Property(e => e.ArtikalId).HasColumnName("artikalID");
            entity.Property(e => e.JedCena).HasColumnName("jedCena");
            entity.Property(e => e.Kolicina).HasColumnName("kolicina");
            entity.Property(e => e.UkupnaVred).HasColumnName("ukupnaVred");

            entity.HasOne(d => d.Artikal).WithMany(p => p.StavkaRacunas)
                .HasForeignKey(d => d.ArtikalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StavkaRacuna_Artikal");
        });

        modelBuilder.Entity<StorniranRacun>(entity =>
        {
            entity.HasKey(e => e.RacunId);

            entity.ToTable("StorniranRacun");

            entity.Property(e => e.RacunId)
                .ValueGeneratedNever()
                .HasColumnName("racunID");
            entity.Property(e => e.Cena).HasColumnName("cena");
            entity.Property(e => e.Datum).HasColumnName("datum");
            entity.Property(e => e.DatumStorniranja).HasColumnName("datumStorniranja");
            entity.Property(e => e.MenadzerUser).HasColumnName("menadzerUser");
            entity.Property(e => e.Pib).HasColumnName("PIB");
            entity.Property(e => e.RadnikUser).HasColumnName("radnikUser");

            entity.HasOne(d => d.MenadzerUserNavigation).WithMany(p => p.StorniranRacuns)
                .HasForeignKey(d => d.MenadzerUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StorniranRacun_Menadzer");

            entity.HasOne(d => d.RadnikUserNavigation).WithMany(p => p.StorniranRacuns)
                .HasForeignKey(d => d.RadnikUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StorniranRacun_Radnik");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
