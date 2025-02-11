using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStore.Models.Domain;
public class Categoria
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public virtual ICollection<Libro>? LibroRelationList { get; set; }
    public virtual ICollection<LibroCategoria>? LibroCategoriaRelationList { get; set; }
	[NotMapped]
	public IEnumerable<SelectListItem>? CategoriasList { get; set; }
}
