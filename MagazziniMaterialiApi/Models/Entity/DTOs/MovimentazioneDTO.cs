using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagazziniMaterialiAPI.Models.Entity.DTOs
{
    public class MovimentazioneDTO
    {
        public int Id { get; set; }
        public string TipoMovimentazione { get; set; } // "Ingresso" o "Uscita"

        [ForeignKey("Materiale")]
        public string CodiceMateriale { get; set; }
        public Materiale Materiale { get; set; }

        [ForeignKey("Magazzino")]
        public int MagazzinoId { get; set; }
        public Magazzino Magazzino { get; set; }

        public int Quantita { get; set; }
        public DateTime DataMovimentazione { get; set; }
        public string Nota { get; set; }
    }
}
