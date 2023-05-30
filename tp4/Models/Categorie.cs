using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace tp4.Models
{
	public class Categorie
	{
		public int CategorieId { get; set; }
		[Required]
		[Display(Name = "Nom")]
		public string Nom { get; set; }
		public virtual ICollection<Produit> Produits { get; set; }

	}
}
