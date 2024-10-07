using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
namespace MagazziniMaterialiAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Magazzino> Magazzini { get; set; }
        public DbSet<Materiale> Materiali { get; set; }
        public DbSet<Classificazione> Classificazioni { get; set; }
        public DbSet<MaterialeImmagine> MaterialeImmagini { get; set; }
        public DbSet<MaterialeMagazzino> MaterialeMagazzini { get; set; }
        public DbSet<MovimentazioneDTO> Movimentazioni { get; set; }
        public DbSet<Giacenza> Giacenze { get; set; }
        public DbSet<MissionePrelievo> MissioniPrelievo { get; set; }
        public DbSet<DettaglioMissione> DettagliMissione { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configura CodiceMateriale come chiave alternativa per la tabella Materiale
            builder.Entity<Materiale>()
                   .HasAlternateKey(m => m.CodiceMateriale);  // Chiave alternativa su CodiceMateriale

            // Configurazioni per Movimentazione
            builder.Entity<MovimentazioneDTO>()
                .HasOne(m => m.Materiale)
                .WithMany()
                .HasForeignKey(m => m.CodiceMateriale)
                .HasPrincipalKey(m => m.CodiceMateriale);  // Relazione tramite CodiceMateriale

            builder.Entity<MovimentazioneDTO>()
                .HasOne(m => m.Magazzino)
                .WithMany()
                .HasForeignKey(m => m.MagazzinoId);

            // Configurazioni per Giacenza
            builder.Entity<Giacenza>()
                .HasOne(g => g.Materiale)
                .WithMany()
                .HasForeignKey(g => g.CodiceMateriale)  // Usa CodiceMateriale come chiave esterna
                .HasPrincipalKey(m => m.CodiceMateriale);  // Relazione tramite CodiceMateriale

            builder.Entity<Giacenza>()
                .HasOne(g => g.Magazzino)
                .WithMany()
                .HasForeignKey(g => g.MagazzinoId);

            // Configurazioni per MissionePrelievo e DettaglioMissione
            builder.Entity<MissionePrelievo>()
                .HasMany(m => m.DettagliMissione)
                .WithOne(d => d.MissionePrelievo)
                .HasForeignKey(d => d.MissionePrelievoId);

            builder.Entity<DettaglioMissione>()
                .HasOne(d => d.Materiale)
                .WithMany()
                .HasForeignKey(d => d.CodiceMateriale)  // Usa CodiceMateriale
                .HasPrincipalKey(m => m.CodiceMateriale);  // Relazione tramite CodiceMateriale

            builder.Entity<MissionePrelievo>()
                .HasOne(m => m.Operatore)
                .WithMany()
                .HasForeignKey(m => m.OperatoreId);

            // Configurazioni per Materiale e le sue Immagini
            builder.Entity<Materiale>()
                .HasMany(m => m.Immagini)  // Collezione di immagini nel materiale
                .WithOne(i => i.Materiale)  // Navigazione inversa: immagine appartiene a un materiale
                .HasForeignKey(i => i.MaterialeId)  // Usa MaterialeId come FK
                .OnDelete(DeleteBehavior.Cascade);  // Cancella immagini associate quando si cancella il materiale

            builder.Entity<MaterialeImmagine>()
                .HasOne(i => i.Materiale)  // Ogni immagine appartiene a un materiale
                .WithMany(m => m.Immagini)  // Il materiale ha molte immagini
                .HasForeignKey(i => i.MaterialeId)  // Chiave esterna MaterialeId
                .IsRequired(true);  // Rendi la FK obbligatoria

            // Configurazioni per MaterialeMagazzino
            builder.Entity<MaterialeMagazzino>()
                .HasOne(mm => mm.Materiale)
                .WithMany(m => m.MaterialeMagazzini)
                .HasForeignKey(mm => mm.CodiceMateriale)
                .HasPrincipalKey(m => m.CodiceMateriale);  // Relazione tramite CodiceMateriale

            builder.Entity<MaterialeMagazzino>()
                .HasOne(mm => mm.Magazzino)
                .WithMany(mg => mg.MaterialeMagazzini)
                .HasForeignKey(mm => mm.MagazzinoID);

            // Configurazioni per Classificazione e Materiale
            builder.Entity<Materiale>()
                .HasMany(m => m.Classificazioni)
                .WithMany(c => c.Materiali);  // Relazione molti-a-molti tra Materiale e Classificazione
        }
    }
}
