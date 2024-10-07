using MagazziniMaterialiAPI.Models.Entity.DTOs;
using MagazziniMaterialiAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MagazziniMaterialiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentazioneController : ControllerBase
    {
        private readonly IMovimentazioneRepository _movimentazioneRepository;
        private readonly IMaterialeRepository _materialeRepository;
        private readonly IGiacenzaRepository _giacenzaRepository;
        private readonly ILogger<MovimentazioneController> _logger;

        public MovimentazioneController(
            IMovimentazioneRepository movimentazioneRepository,
            IMaterialeRepository materialeRepository,
            IGiacenzaRepository giacenzaRepository,
            ILogger<MovimentazioneController> logger)
        {
            _movimentazioneRepository = movimentazioneRepository ?? throw new ArgumentNullException(nameof(movimentazioneRepository));
            _materialeRepository = materialeRepository ?? throw new ArgumentNullException(nameof(materialeRepository));
            _giacenzaRepository = giacenzaRepository ?? throw new ArgumentNullException(nameof(giacenzaRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("ingresso")]
        public IActionResult MovimentazioneIngresso([FromBody] MovimentazioneDTO movimentazioneDTO)
        {
            if (movimentazioneDTO == null)
            {
                return BadRequest("Dati di movimentazione mancanti.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            var materiale = _materialeRepository.GetByCodiceMateriale(movimentazioneDTO.CodiceMateriale);

            if (materiale == null)
            {
                return NotFound("Materiale non trovato.");
            }

            
            _movimentazioneRepository.Add(movimentazioneDTO);

            
            _giacenzaRepository.AggiornaGiacenza(movimentazioneDTO.MagazzinoId, movimentazioneDTO.CodiceMateriale, movimentazioneDTO.Quantita);

            _logger.LogInformation($"Movimentazione di ingresso per il materiale {movimentazioneDTO.CodiceMateriale} registrata con successo.");

            return Ok("Movimentazione di ingresso registrata con successo.");
        }

        [HttpPost("uscita")]
        public IActionResult MovimentazioneUscita([FromBody] MovimentazioneDTO movimentazione)
        {
            if (movimentazione == null)
            {
                return BadRequest("Dati di movimentazione mancanti.");
            }

            var giacenza = _giacenzaRepository.GetGiacenza(movimentazione.MagazzinoId, movimentazione.CodiceMateriale);

            if (giacenza == null)
            {
                return NotFound("Giacenza non trovata.");
            }

            if (giacenza.QuantitaDisponibile < movimentazione.Quantita)
            {
                return BadRequest("Quantità insufficiente in magazzino.");
            }

            
            _giacenzaRepository.AggiornaGiacenza(movimentazione.MagazzinoId, movimentazione.CodiceMateriale, -movimentazione.Quantita);

            
            _movimentazioneRepository.Add(movimentazione);

            _logger.LogInformation($"Movimentazione di uscita per il materiale {movimentazione.CodiceMateriale} registrata con successo.");

            return Ok("Movimentazione di uscita registrata con successo.");
        }

        [HttpDelete("{id}/storno")]
        public IActionResult StornaMovimentazione(int id)
        {
            var movimentazione = _movimentazioneRepository.GetById(id);

            if (movimentazione == null)
            {
                return NotFound("Movimentazione non trovata.");
            }

            // Controlla se ci sono movimentazioni successive
            var hasMovimentazioniSuccessive = _movimentazioneRepository.EsisteMovimentazioneSuccessiva(movimentazione.CodiceMateriale, movimentazione.DataMovimentazione);

            if (hasMovimentazioniSuccessive)
            {
                return BadRequest("Non è possibile stornare la movimentazione. Esistono movimentazioni successive.");
            }

            // Storna la movimentazione (aggiunge la quantità in uscita, sottrae la quantità in ingresso)
            if (movimentazione.TipoMovimentazione == "Ingresso")
            {
                _giacenzaRepository.AggiornaGiacenza(movimentazione.MagazzinoId, movimentazione.CodiceMateriale, -movimentazione.Quantita);
            }
            else if (movimentazione.TipoMovimentazione == "Uscita")
            {
                _giacenzaRepository.AggiornaGiacenza(movimentazione.MagazzinoId, movimentazione.CodiceMateriale, movimentazione.Quantita);
            }

            // Elimina la movimentazione
            _movimentazioneRepository.Delete(movimentazione.Id);

            _logger.LogInformation($"Movimentazione {id} stornata con successo.");

            return NoContent();
        }
        [HttpGet]
        public ActionResult<IEnumerable<MovimentazioneDTO>> GetAll([FromQuery] string? codiceMateriale = null)
        {
            try
            {
                IEnumerable<MovimentazioneDTO> movimentazioni;

                if (!string.IsNullOrEmpty(codiceMateriale))
                {
                    movimentazioni = _movimentazioneRepository.GetByMaterialeId(codiceMateriale);
                }
                else
                {
                    movimentazioni = _movimentazioneRepository.GetAllAsync();
                }

                var movimentazioniDTO = movimentazioni.Select(m => new MovimentazioneDTO
                {
                    Id = m.Id,
                    TipoMovimentazione = m.TipoMovimentazione,
                    CodiceMateriale = m.CodiceMateriale,
                    MagazzinoId = m.MagazzinoId,
                    Quantita = m.Quantita,
                    DataMovimentazione = m.DataMovimentazione,
                    Nota = m.Nota
                }).ToList();

                return Ok(movimentazioniDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle movimentazioni");
                return StatusCode(500, "Si è verificato un errore durante il recupero delle movimentazioni");
            }
        }
    }
    
}

