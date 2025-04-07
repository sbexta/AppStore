using AppStore.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace AppStore.Repositories.Interfaz
{
    public interface ICategoriaRepository
    {
        IQueryable<Categoria> List();
        bool Add(Categoria categoria);
        IEnumerable<Categoria> Search(string term);
    }
}
