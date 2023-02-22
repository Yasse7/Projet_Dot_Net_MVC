using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AchatEnLigne.Models
{
    public class Panier
    {
        public int Id { get; set; }
        [Range(1, Int32.MaxValue)]
        public int NombreArticles { get; set; }
        public float Total { get; set; }
        
        public int Produit { get; set; }
        
        public int User { get; set; }
    }
}
