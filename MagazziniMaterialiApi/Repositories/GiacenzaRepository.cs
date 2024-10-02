using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;

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
        public async Task<Giacenza> GetByIdAsync(int id)
        {
            return await _context.Giacenze.FindAsync(id);
        }

        public async Task<IEnumerable<Giacenza>> GetAllAsync()
        {
            return await _context.Giacenze.ToListAsync();
        }

        public async Task<IEnumerable<Giacenza>> GetByMagazzinoIdAsync(int magazzinoId)
        {
            return await _context.Giacenze
                .Where(g => g.MagazzinoId == magazzinoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Giacenza>> GetByCodiceMaterialeAsync(string codiceMateriale)
        {
            return await _context.Giacenze
                .Where(g => g.CodiceMateriale == codiceMateriale)
                .ToListAsync();
        }

        public async Task<Giacenza> GetByMagazzinoAndMaterialeAsync(int magazzinoId, string codiceMateriale)
        {
            return await _context.Giacenze
                .FirstOrDefaultAsync(g => g.MagazzinoId == magazzinoId && g.CodiceMateriale == codiceMateriale);
        }

        public async Task AddAsync(Giacenza giacenza)
        {
            await _context.Giacenze.AddAsync(giacenza);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Giacenza giacenza)
        {
            _context.Giacenze.Update(giacenza);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var giacenza = await GetByIdAsync(id);
            if (giacenza != null)
            {
                _context.Giacenze.Remove(giacenza);
                await _context.SaveChangesAsync();
            }
        }
    }
}
