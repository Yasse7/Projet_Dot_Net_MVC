namespace AchatEnLigne.Models
{
    public class LignePanierCommande
    {
        public int LignepanierCommandeId { get; set; }
        public int Qte { get; set; }
        public int? UserId { get; set; }
        public int? CommandeId { get; set; }
        public float Total { get; set; }
        public int UserX { get; set; }
        public int CommandeX { get; set; }
        public User? User { get; set; }
        public Commande? Commande { get; set; }

        
    }
}
