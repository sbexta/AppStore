using AppStore.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace AppStore.Repositories.Abstract
{
    public interface ILibroRepository
    {
        bool Add(Libro libro);
        bool Delete(int id);
        Libro GetById(int id);
        IQueryable<Libro> List();
        bool Update(Libro libro);
        List<int> GetCategoriaByLibroId(int libroId);
        List<Categoria> GetCategorias(); // Nuevo método
        List<LibroCategoria> GetLibroCategorias(); // Nuevo método
    }
}
