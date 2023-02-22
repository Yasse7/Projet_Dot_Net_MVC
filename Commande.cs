namespace AchatEnLigne.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public string Adresse { get; set; }
        public Panier? Panier { get; set; }
        public int? PanierId { get; set; }
        public int NumCommande { get; set; }
        public int produit { get; set; }
        public int NombreArticles { get; set; }
        public int User { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<LignePanierCommande>? LignePanierCommande { get; set; }
    }
}
