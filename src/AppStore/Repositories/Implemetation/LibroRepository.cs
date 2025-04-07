using AppStore.Models.Domain;
using AppStore.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AppStore.Repositories.Implementation
{
    public class LibroRepository : ILibroRepository
    {
        private readonly DataBaseContext _context;

        public LibroRepository(DataBaseContext context)
        {
            _context = context;
        }

        public bool Add(Libro libro)
        {
            try
            {
                _context.Libros!.Add(libro);
                _context.SaveChanges();

                foreach (int categoriaId in libro.Categorias!)
                {
                    var libroCategoria = new LibroCategoria
                    {
                        LibroId = libro.Id,
                        CategoriaId = categoriaId
                    };
                    _context.LibroCategorias!.Add(libroCategoria);
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var libro = GetById(id);
                if (libro is null)
                {
                    return false;
                }

                var libroCategorias = _context.LibroCategorias!.Where(a => a.LibroId == libro.Id);
                _context.LibroCategorias!.RemoveRange(libroCategorias);
                _context.Libros!.Remove(libro);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Libro GetById(int id)
        {
            return _context.Libros!.Find(id)!;
        }

        public IQueryable<Libro> List()
        {
            return _context.Libros!.AsQueryable();
        }

        public bool Update(Libro libro)
        {
            try
            {
                var categoriasParaEliminar = _context.LibroCategorias!.Where(a => a.LibroId == libro.Id);
                _context.LibroCategorias!.RemoveRange(categoriasParaEliminar);

                foreach (int categoriaId in libro.Categorias!)
                {
                    var libroCategoria = new LibroCategoria { LibroId = libro.Id, CategoriaId = categoriaId };
                    _context.LibroCategorias!.Add(libroCategoria);
                }

                _context.Libros!.Update(libro);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<int> GetCategoriaByLibroId(int libroId)
        {
            return _context.LibroCategorias!
                .Where(a => a.LibroId == libroId)
                .Select(a => a.CategoriaId)
                .ToList();
        }

        public List<Categoria> GetCategorias() // Implementación del nuevo método
        {
            return _context.Categorias!.ToList();
        }

        public List<LibroCategoria> GetLibroCategorias() // Implementación del nuevo método
        {
            return _context.LibroCategorias!.ToList();
        }
    }
}
