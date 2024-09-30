using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Repositories;
using Microsoft.AspNetCore.Http;
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
        public IActionResult MovimentazioneIngresso([FromBody] Movimentazione movimentazione)
        {
            var materiale = _materialeRepository.GetByCodiceMateriale(movimentazione.CodiceMateriale);

            if (materiale == null)
            {
                return NotFound("Materiale non trovato.");
            }

            // Aggiungi la movimentazione nel sistema
            _movimentazioneRepository.Add(movimentazione);

            // Aggiorna la giacenza aggiungendo la quantità
            _giacenzaRepository.AggiornaGiacenza(movimentazione.MagazzinoId, movimentazione.CodiceMateriale, movimentazione.Quantita);

            _logger.LogInformation($"Movimentazione di ingresso per il materiale {movimentazione.CodiceMateriale} registrata con successo.");

            return Ok();
        }

        [HttpPost("uscita")]
        public IActionResult MovimentazioneUscita([FromBody] Movimentazione movimentazione)
        {
            var giacenza = _giacenzaRepository.GetGiacenza(movimentazione.MagazzinoId, movimentazione.CodiceMateriale);

            if (giacenza == null || giacenza.QuantitaDisponibile < movimentazione.Quantita)
            {
                return BadRequest("Quantità insufficiente in magazzino.");
            }

            // Aggiorna la giacenza sottraendo la quantità
            _giacenzaRepository.AggiornaGiacenza(movimentazione.MagazzinoId, movimentazione.CodiceMateriale, -movimentazione.Quantita);

            // Aggiungi la movimentazione
            _movimentazioneRepository.Add(movimentazione);

            _logger.LogInformation($"Movimentazione di uscita per il materiale {movimentazione.CodiceMateriale} registrata con successo.");

            return Ok();
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

            _movimentazioneRepository.Delete(movimentazione.Id);

            _logger.LogInformation($"Movimentazione {id} stornata con successo.");

            return NoContent();
        }
    }
}
