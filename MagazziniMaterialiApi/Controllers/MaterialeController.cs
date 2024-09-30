using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;
using MagazziniMaterialiAPI.Services;
using Microsoft.Extensions.Logging;

namespace MagazziniMaterialiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialeController : ControllerBase
    {
        private readonly IMaterialiService _materialiService;
        private readonly IMaterialeMapper _materialeMapper;
        private readonly EtichettaService _etichettaService;
        private readonly ILogger<MaterialeController> _logger;

        public MaterialeController(IMaterialiService materialiService, IMaterialeMapper materialeMapper, EtichettaService etichettaService, ILogger<MaterialeController> logger)
        {
            _materialiService = materialiService ?? throw new ArgumentNullException(nameof(materialiService));
            _materialeMapper = materialeMapper ?? throw new ArgumentNullException(nameof(materialeMapper));
            _etichettaService = etichettaService ?? throw new ArgumentNullException(nameof(etichettaService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Materiale
        [HttpGet]
        public ActionResult<IEnumerable<MaterialeDTO>> GetMateriali()
        {
            var materiali = _materialiService.GetAll();
            return Ok(materiali);
        }

        // GET: api/Materiale/{CodiceMateriale}
        [HttpGet("{codiceMateriale}")]
        public ActionResult<MaterialeDTO> GetMaterialeByCodiceMateriale(string codiceMateriale)
        {
            var materiale = _materialiService.GetByCodiceMateriale(codiceMateriale);
            if (materiale == null) return NotFound($"Materiale con codice '{codiceMateriale}' non trovato.");
            return Ok(materiale);
        }

        // POST: api/Materiale
        [HttpPost]
        public ActionResult<MaterialeDTO> AddMateriale([FromBody] MaterialeDTO materialeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (_materialiService.ExistsByCodice(materialeDTO.CodiceMateriale))
                {
                    return Conflict($"Un materiale con il codice '{materialeDTO.CodiceMateriale}' esiste già.");
                }

                var nuovoMateriale = _materialiService.AddMateriale(materialeDTO);

                // Genera il codice QR
                string qrCodeData = GeneraCodiceQR(nuovoMateriale.CodiceMateriale);

                // Aggiungi immagine del QR code
                var immagineQRCode = new MaterialeImmagine
                {
                    UrlImmagine = string.Empty,
                    IsPrincipale = true,
                    QRCodeData = qrCodeData
                };

                nuovoMateriale.Immagini ??= new List<MaterialeImmagine>();
                nuovoMateriale.Immagini.Add(immagineQRCode);

                // Imposta la data di creazione e associa il materiale ai magazzini
                nuovoMateriale.DataCreazione = DateTime.UtcNow;

                _materialiService.SaveChanges();

                // Mappa il risultato al DTO
                var materialeDTOResult = _materialeMapper.MapToMaterialeDTO(nuovoMateriale);
                return CreatedAtAction(nameof(GetMaterialeByCodiceMateriale), new { codiceMateriale = nuovoMateriale.CodiceMateriale }, materialeDTOResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del materiale: {Message}", ex.Message);
                return StatusCode(500, "Si è verificato un errore interno durante la creazione del materiale.");
            }
        }

        // PUT: api/Materiale/{CodiceMateriale}
        [HttpPut("{codiceMateriale}")]
        public ActionResult EditMateriale(string codiceMateriale, [FromBody] MaterialeDTO materialeDTO)
        {
            var isEdited = _materialiService.EditMateriale(codiceMateriale, materialeDTO);
            if (!isEdited) return NotFound("Materiale non trovato.");

            _materialiService.SaveChanges();
            return Ok(materialeDTO);
        }

        // DELETE: api/Materiale/{CodiceMateriale}
        [HttpDelete("{codiceMateriale}")]
        public ActionResult DeleteMateriale(string codiceMateriale)
        {
            var isDeleted = _materialiService.DeleteMateriale(codiceMateriale);
            if (!isDeleted) return NotFound("Materiale non trovato.");

            _materialiService.SaveChanges();
            return Ok("Materiale eliminato correttamente.");
        }

        private string GeneraCodiceQR(string codiceMateriale)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(codiceMateriale, QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
                byte[] qrCodeImage = qrCode.GetGraphic(20);
                return Convert.ToBase64String(qrCodeImage);
            }
        }
    }
}
