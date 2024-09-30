using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagazziniMaterialiAPI.Models.Entity
{
    public class MissionePrelievo
    {
        public int Id { get; set; }
        public string CodiceUnivoco { get; set; }
        public string TipologiaDestinazione { get; set; }
        public string Descrizione { get; set; }
        public string Stato { get; set; } // "Attiva", "Sospesa", "Completata"
        public string OperatoreId { get; set; }

        [ForeignKey("OperatoreId")]
        public IdentityUser Operatore { get; set; }
        public List<DettaglioMissione> DettagliMissione { get; set; }
    }

    public class DettaglioMissione
    {
        public int Id { get; set; }

        [ForeignKey("MissionePrelievo")]
        public int MissionePrelievoId { get; set; }
        public MissionePrelievo MissionePrelievo { get; set; }

        [ForeignKey("Materiale")]
        public string CodiceMateriale { get; set; }
        public Materiale Materiale { get; set; }

        public int QuantitaPrelevata { get; set; }
    }
}
