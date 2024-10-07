using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MagazziniMaterialiAPI.Models.Entity
{
    public class Classificazione
    {
        [Key]
        public string CodiceClassificazione { get; set; }
        public string NomeClassificazione { get; set; }
        [JsonIgnore]
        public ICollection<Materiale> Materiali { get; set; }
    }
}
