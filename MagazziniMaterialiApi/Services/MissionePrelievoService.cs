using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace MagazziniMaterialiAPI.Services
{
    public class MissionePrelievoService
    {
        private readonly ApplicationDbContext _context;

        public MissionePrelievoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public MissionePrelievo CreaMissione(string tipologiaDestinazione, string descrizione, string operatoreId)
        {
            var missione = new MissionePrelievo
            {
                CodiceUnivoco = GeneraCodiceUnivoco(),
                TipologiaDestinazione = tipologiaDestinazione,
                Descrizione = descrizione,
                Stato = "Attiva",
                OperatoreId = operatoreId,
                DettagliMissione = new List<DettaglioMissione>()
            };

            _context.MissioniPrelievo.Add(missione);
            _context.SaveChanges();

            return missione;
        }

        public void AggiungiMaterialeAMissione(int missioneId, string codiceMateriale, int quantita)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var missione = _context.MissioniPrelievo.Find(missioneId);
                if (missione == null || missione.Stato != "Attiva")
                {
                    throw new InvalidOperationException("Missione non trovata o non attiva.");
                }

                var giacenza = _context.Giacenze.FirstOrDefault(g => g.CodiceMateriale == codiceMateriale);
                if (giacenza == null || giacenza.QuantitaDisponibile < quantita)
                {
                    throw new InvalidOperationException("Giacenza insufficiente per il prelievo del materiale.");
                }

                giacenza.QuantitaDisponibile -= quantita;
                giacenza.QuantitaImpegnata += quantita;

                var dettaglio = new DettaglioMissione
                {
                    MissionePrelievoId = missioneId,
                    CodiceMateriale = codiceMateriale,
                    QuantitaPrelevata = quantita
                };
                missione.DettagliMissione.Add(dettaglio);

                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void CompletaMissione(int missioneId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var missione = _context.MissioniPrelievo
                    .Include(m => m.DettagliMissione)
                    .FirstOrDefault(m => m.Id == missioneId);

                if (missione == null || missione.Stato != "Attiva")
                {
                    throw new InvalidOperationException("Missione non trovata o non attiva.");
                }

                missione.Stato = "Completata";

                foreach (var dettaglio in missione.DettagliMissione)
                {
                    var giacenza = _context.Giacenze.FirstOrDefault(g => g.CodiceMateriale == dettaglio.CodiceMateriale);
                    giacenza.QuantitaImpegnata -= dettaglio.QuantitaPrelevata;
                }

                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private string GeneraCodiceUnivoco()
        {
            // Implementa la logica per generare un codice univoco
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }
    }
}
