using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;

namespace MagazziniMaterialiAPI.Services
{
    public class MovimentazioneService
    {
        private readonly ApplicationDbContext _context;

        public MovimentazioneService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void RegistraMovimentazioneIngresso(string codiceMateriale, int magazzinoId, int quantita, string nota)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var giacenza = _context.Giacenze.FirstOrDefault(g => g.CodiceMateriale == codiceMateriale && g.MagazzinoId == magazzinoId);
                if (giacenza == null)
                {
                    giacenza = new Giacenza
                    {
                        CodiceMateriale = codiceMateriale,
                        MagazzinoId = magazzinoId,
                        QuantitaDisponibile = 0,
                        QuantitaImpegnata = 0
                    };
                    _context.Giacenze.Add(giacenza);
                }

                giacenza.QuantitaDisponibile += quantita;

                var movimentazione = new MovimentazioneDTO
                {
                    TipoMovimentazione = "Ingresso",
                    CodiceMateriale = codiceMateriale,
                    MagazzinoId = magazzinoId,
                    Quantita = quantita,
                    DataMovimentazione = DateTime.Now,
                    Nota = nota
                };
                _context.Movimentazioni.Add(movimentazione);

                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void RegistraMovimentazioneUscita(string codiceMateriale, int magazzinoId, int quantita, string nota)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var giacenza = _context.Giacenze.FirstOrDefault(g => g.CodiceMateriale == codiceMateriale && g.MagazzinoId == magazzinoId);
                if (giacenza == null || giacenza.QuantitaDisponibile < quantita)
                {
                    throw new InvalidOperationException("Giacenza insufficiente per l'uscita del materiale.");
                }

                giacenza.QuantitaDisponibile -= quantita;

                var movimentazione = new MovimentazioneDTO
                {
                    TipoMovimentazione = "Uscita",
                    CodiceMateriale = codiceMateriale,
                    MagazzinoId = magazzinoId,
                    Quantita = quantita,
                    DataMovimentazione = DateTime.Now,
                    Nota = nota
                };
                _context.Movimentazioni.Add(movimentazione);

                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
