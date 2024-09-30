using System.ComponentModel.DataAnnotations.Schema;

namespace MagazziniMaterialiAPI.Models.Entity
{
    public class Giacenza
    {
        public int Id { get; set; }

        [ForeignKey("Materiale")]
        public string CodiceMateriale { get; set; }
        public Materiale Materiale { get; set; }

        [ForeignKey("Magazzino")]
        public int MagazzinoId { get; set; }
        public Magazzino Magazzino { get; set; }

        public int QuantitaDisponibile { get; set; }
        public int QuantitaImpegnata { get; set; }
    }
}
