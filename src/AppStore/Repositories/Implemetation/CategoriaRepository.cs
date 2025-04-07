using AppStore.Models.Domain;
using AppStore.Repositories.Implementation;
using AppStore.Repositories.Interfaz;
using Microsoft.EntityFrameworkCore;

namespace AppStore.Repositories.Implementation
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DataBaseContext _context;

        public CategoriaRepository(DataBaseContext context)
        {
            _context = context;
        }

        public IQueryable<Categoria> List()
        {
            return _context.Categorias.AsQueryable();
        }

        public bool Add(Categoria categoria)
        {
            try
            {
                _context.Add(categoria);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Categoria> Search(string term)
        {
            term = term?.ToLower() ?? string.Empty;
            return _context.Categorias
                .Where(a => a.Nombre!.StartsWith(term))
                .ToList();
        }
    }
}

