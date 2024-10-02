namespace MagazziniMaterialiCLient.Models
{
    public class DettagliMissioneViewModel
    {
        public int Id { get; set; }
        public string CodiceUnivoco { get; set; }
        public string TipologiaDestinazione { get; set; }
        public string Descrizione { get; set; }
        public string Stato { get; set; }
        public OperatoreViewModel Operatore { get; set; }
    }

    public class OperatoreViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
