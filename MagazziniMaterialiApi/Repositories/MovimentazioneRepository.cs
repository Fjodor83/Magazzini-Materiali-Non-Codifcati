using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MagazziniMaterialiAPI.Repositories
{
    public class MovimentazioneRepository : IMovimentazioneRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MovimentazioneRepository> _logger;

        public MovimentazioneRepository(ApplicationDbContext context, ILogger<MovimentazioneRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Movimentazione GetById(int id)
        {
            return _context.Movimentazioni
                .Include(m => m.Materiale)
                .Include(m => m.Magazzino)
                .FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Movimentazione> GetAll()
        {
            return _context.Movimentazioni
                .Include(m => m.Materiale)
                .Include(m => m.Magazzino)
                .ToList();
        }

        public IEnumerable<Movimentazione> GetByMaterialeId(string codiceMateriale)
        {
            return _context.Movimentazioni
                .Include(m => m.Materiale)
                .Where(m => m.CodiceMateriale == codiceMateriale)
                .ToList();
        }

        public void Add(Movimentazione movimentazione)
        {
            if (movimentazione == null)
            {
                throw new ArgumentNullException(nameof(movimentazione));
            }

            try
            {
                _context.Movimentazioni.Add(movimentazione);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiunta della movimentazione: {Message}", ex.InnerException?.Message ?? ex.Message);
                throw new InvalidOperationException("Errore durante l'aggiunta della movimentazione.", ex);
            }
        }

        public void Delete(int id)
        {
            var movimentazione = _context.Movimentazioni.Find(id);
            if (movimentazione == null)
            {
                throw new KeyNotFoundException($"La movimentazione con ID {id} non esiste.");
            }

            try
            {
                _context.Movimentazioni.Remove(movimentazione);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Errore durante l'eliminazione della movimentazione: {Message}", ex.InnerException?.Message ?? ex.Message);
                throw new InvalidOperationException("Errore durante l'eliminazione della movimentazione.", ex);
            }
        }

        public void Update(Movimentazione movimentazione)
        {
            if (movimentazione == null)
            {
                throw new ArgumentNullException(nameof(movimentazione));
            }

            var existingMovimentazione = _context.Movimentazioni.Find(movimentazione.Id);
            if (existingMovimentazione == null)
            {
                throw new KeyNotFoundException($"La movimentazione con ID {movimentazione.Id} non esiste.");
            }

            try
            {
                _context.Entry(existingMovimentazione).CurrentValues.SetValues(movimentazione);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento della movimentazione: {Message}", ex.InnerException?.Message ?? ex.Message);
                throw new InvalidOperationException("Errore durante l'aggiornamento della movimentazione.", ex);
            }
        }

        public bool EsisteMovimentazioneSuccessiva(string codiceMateriale, DateTime dataMovimentazione)
        {
            return _context.Movimentazioni
                .Any(m => m.CodiceMateriale == codiceMateriale && m.DataMovimentazione > dataMovimentazione);
        }
    }
}
