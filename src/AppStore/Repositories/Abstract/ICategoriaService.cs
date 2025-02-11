using AppStore.Models.Domain;
using AppStore.Models.DTO;

namespace AppStore.Repositories.Abstract;

public interface ICategoriaService
{
    IQueryable<Categoria> ListCategorias();
    bool Add(Categoria categoria);
	CategoriaListVm List(string term = "", bool paging = false, int currentPage = 0);
}