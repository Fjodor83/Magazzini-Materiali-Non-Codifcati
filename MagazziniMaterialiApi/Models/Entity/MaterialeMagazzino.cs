using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MagazziniMaterialiAPI.Models.Entity
{
    public class MaterialeMagazzino
    {
        public int MaterialeMagazzinoID { get; set; }

        [ForeignKey("Materiale")]
        public string CodiceMateriale { get; set; }
        public Materiale Materiale { get; set; }

        [ForeignKey("Magazzino")]
        public int MagazzinoID { get; set; }

        [JsonIgnore]
        public Magazzino Magazzino { get; set; }
    }
}