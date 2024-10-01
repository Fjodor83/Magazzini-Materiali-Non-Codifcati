using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MagazziniMaterialiAPI.Repositories
{
    public class MaterialeRepository : BaseRepository, IMaterialeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MaterialeRepository> _logger;

        public MaterialeRepository(ApplicationDbContext context, ILogger<MaterialeRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public List<Materiale> GetAll()
        {
            return _context.Materiali
                .Include(m => m.Immagini)
                .Include(m => m.Classificazioni)
                .ToList();
        }

        public Materiale? GetByCodiceMateriale(string codiceMateriale)
        {
            return _context.Materiali
                .Include(m => m.Immagini)
                .Include(m => m.Classificazioni)
                .FirstOrDefault(m => m.CodiceMateriale == codiceMateriale);
        }

        public Materiale AddMateriale(Materiale materiale)
        {
            _context.Materiali.Add(materiale);
            return materiale;
        }

        public void DeleteMateriale(Materiale materiale)
        {
            _context.Materiali.Remove(materiale);
        }

        public bool EditMateriale(string codiceMateriale, Materiale materiale)
        {
            
            var existingEntity = _context.Materiali.FirstOrDefault(m => m.CodiceMateriale == codiceMateriale);

           
            if (existingEntity == null)
            {
                return false;
            }

            // Aggiorna le proprietà dell'entità esistente con i valori dell'entità 'materiale' ricevuta
            existingEntity.CodiceMateriale = materiale.CodiceMateriale;
            existingEntity.Descrizione = materiale.Descrizione;
            existingEntity.Note = materiale.Note;
            existingEntity.DataCreazione = materiale.DataCreazione;

           

            // Salva i cambiamenti nel database
            _context.SaveChanges();

            return true;
        }


        public bool ExistsByCodice(string codiceMateriale)
        {
            return _context.Materiali.Any(m => m.CodiceMateriale == codiceMateriale);
        }

        

        public List<Magazzino> GetMagazziniByMaterialeId(string codiceMateriale)
        {
            var materiale = _context.Materiali
                .Include(m => m.MaterialeMagazzini)
                .ThenInclude(mm => mm.Magazzino)
                .FirstOrDefault(m => m.CodiceMateriale == codiceMateriale);

            return materiale?.MaterialeMagazzini.Select(mm => mm.Magazzino).ToList() ?? new List<Magazzino>();
        }

        public new void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
