using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity.DTOs;
using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissioniController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MissioniController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpPost("crea-missione")]
        public async Task<IActionResult> CreaMissione([FromBody] CreaMissioneDto dto)
        {
            var operatore = await _userManager.FindByIdAsync(dto.OperatoreId);
            if (operatore == null)
            {
                return BadRequest("Operatore non trovato");
            }

            if (!await _userManager.IsInRoleAsync(operatore, "Operatore"))
            {
                return BadRequest("L'utente specificato non è un operatore");
            }

            var missione = new MissionePrelievo
            {
                TipologiaDestinazione = dto.TipologiaDestinazione,
                Descrizione = dto.Descrizione,
                Stato = "Attiva",
                OperatoreId = dto.OperatoreId,
                CodiceUnivoco = GeneraCodiceUnivoco() 
            };

            _context.MissioniPrelievo.Add(missione);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Missione creata con successo", missioneId = missione.Id });
        }
        [HttpGet("missioni-operatore/{operatoreId}")]
        public async Task<IActionResult> GetMissioniOperatore(string operatoreId)
        {
            var missioni = await _context.MissioniPrelievo
                .Where(m => m.OperatoreId == operatoreId)
                .ToListAsync();

            return Ok(missioni);
        }
        [HttpGet("dettagli-missione/{id}")]
        public async Task<IActionResult> GetDettagliMissione(int id)
        {
            var missione = await _context.MissioniPrelievo
                .Include(m => m.Operatore)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (missione == null)
            {
                return NotFound();
            }

            var dettagli = new
            {
                missione.Id,
                missione.CodiceUnivoco,
                missione.TipologiaDestinazione,
                missione.Descrizione,
                missione.Stato,
                Operatore = new
                {
                    Id = missione.OperatoreId,
                    missione.Operatore.UserName,
                    missione.Operatore.Email
                }
            };

            return Ok(dettagli);
        }
        [HttpPut("aggiorna-operatore-missione")]
        public async Task<IActionResult> AggiornaMissioneOperatore(int missioneId, string nuovoOperatoreId)
        {
            var missione = await _context.MissioniPrelievo.FindAsync(missioneId);
            if (missione == null)
            {
                return NotFound("Missione non trovata");
            }

            var nuovoOperatore = await _userManager.FindByIdAsync(nuovoOperatoreId);
            if (nuovoOperatore == null)
            {
                return BadRequest("Nuovo operatore non trovato");
            }

            missione.OperatoreId = nuovoOperatoreId;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Operatore della missione aggiornato con successo" });
        }

        // [Authorize(Roles = "Amministratore,Operatore")]
        [HttpGet("missioni")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissionePrelievo>>> GetMissioni()
        {
            try
            {
                var missioni = await _context.MissioniPrelievo.ToListAsync();
                return Ok(missioni);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Si è verificato un errore interno del server");
            }
        }

        // Implementa questo metodo per generare un codice univoco
        private string GeneraCodiceUnivoco()
        {
            // Logica per generare un codice univoco
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }
    }
}