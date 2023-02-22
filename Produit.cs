using System.ComponentModel.DataAnnotations;

namespace AchatEnLigne.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Range(0, Int32.MaxValue)]
        public int Stock { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
        public string URL { get; set; }

        public DateTime? CreatedDate { get; set; }
        
    }
}
