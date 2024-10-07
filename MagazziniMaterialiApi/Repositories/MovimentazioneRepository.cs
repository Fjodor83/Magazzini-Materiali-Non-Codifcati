using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;
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

        public MovimentazioneDTO GetById(int id)
        {
            return _context.Movimentazioni
                .Include(m => m.Materiale)
                .Include(m => m.Magazzino)
                .FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<MovimentazioneDTO> GetAllAsync()
        {
            return (IEnumerable<MovimentazioneDTO>)_context.Movimentazioni
                .Include(m => m.Materiale)
                .Include(m => m.Magazzino)
                .ToList();
        }

        public IEnumerable<MovimentazioneDTO> GetByMaterialeId(string codiceMateriale)
        {
            return _context.Movimentazioni
                .Include(m => m.Materiale)
                .Where(m => m.CodiceMateriale == codiceMateriale)
                .ToList();
        }
/*
        public void Add(MovimentazioneDTO movimentazione)
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
*/
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

        public void Update(MovimentazioneDTO movimentazione)
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

        public void Add(MovimentazioneDTO movimentazioneDTO)
        {
            if (movimentazioneDTO == null)
            {
                throw new ArgumentNullException(nameof(movimentazioneDTO));
            }

            try
            {
                var movimentazione = new MovimentazioneDTO
                {
                    TipoMovimentazione = movimentazioneDTO.TipoMovimentazione,
                    CodiceMateriale = movimentazioneDTO.CodiceMateriale,
                    MagazzinoId = movimentazioneDTO.MagazzinoId,
                    Quantita = movimentazioneDTO.Quantita,
                    DataMovimentazione = movimentazioneDTO.DataMovimentazione,
                    Nota = movimentazioneDTO.Nota
                };

                // Carica il Materiale e il Magazzino correlati
                var materiale = _context.Materiali.FirstOrDefault(m => m.CodiceMateriale == movimentazioneDTO.CodiceMateriale);
                var magazzino = _context.Magazzini.Find(movimentazioneDTO.MagazzinoId);

                if (materiale == null)
                {
                    throw new InvalidOperationException($"Materiale con codice {movimentazioneDTO.CodiceMateriale} non trovato.");
                }

                if (magazzino == null)
                {
                    throw new InvalidOperationException($"Magazzino con ID {movimentazioneDTO.MagazzinoId} non trovato.");
                }

                movimentazione.Materiale = materiale;
                movimentazione.Magazzino = magazzino;

                _context.Movimentazioni.Add(movimentazione);
                _context.SaveChanges();

                _logger.LogInformation($"Movimentazione aggiunta con successo: {movimentazione.Id}");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiunta della movimentazione: {Message}", ex.InnerException?.Message ?? ex.Message);
                throw new InvalidOperationException("Errore durante l'aggiunta della movimentazione.", ex);
            }
        }
    }
}
