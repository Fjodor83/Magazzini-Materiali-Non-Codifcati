using System.ComponentModel.DataAnnotations;
namespace MagazziniMaterialiCLient.Models
{
    public class Magazzino
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Il codice è obbligatorio")]
        public string CodiceMagazzino { get; set; }
        [Required(ErrorMessage = "Il nome è obbligatorio")]
        public string NomeMagazzino { get; set; }
        public string DescrizioneMagazzino { get; set; }
        public string Note { get; set; }
        public List<MaterialeMagazzino> MaterialeMagazzini { get; set; }

        public Magazzino()
        {
            MaterialeMagazzini = new List<MaterialeMagazzino>();
        }
    }
}
