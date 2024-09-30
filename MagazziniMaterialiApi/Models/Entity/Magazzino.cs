namespace MagazziniMaterialiAPI.Models.Entity
{
    public class Magazzino
    {
        public int Id { get; set; }
        public string CodiceMagazzino { get; set; }
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