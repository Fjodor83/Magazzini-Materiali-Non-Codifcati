using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Repositories
{
    public class GiacenzaRepository : IGiacenzaRepository
    {
        private readonly ApplicationDbContext _context;

        public GiacenzaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Giacenza GetGiacenza(int magazzinoId, string codiceMateriale)
        {
            return _context.Giacenze
                .FirstOrDefault(g => g.MagazzinoId == magazzinoId && g.CodiceMateriale == codiceMateriale);
        }

        public void AggiornaGiacenza(int magazzinoId, string codiceMateriale, int quantita)
        {
            var giacenza = GetGiacenza(magazzinoId, codiceMateriale);
            if (giacenza == null)
            {
                giacenza = new Giacenza
                {
                    MagazzinoId = magazzinoId,
                    CodiceMateriale = codiceMateriale,
                    QuantitaDisponibile = quantita
                };
                _context.Giacenze.Add(giacenza);
            }
            else
            {
                giacenza.QuantitaDisponibile += quantita;
            }

            _context.SaveChanges();
        }
    }
}
