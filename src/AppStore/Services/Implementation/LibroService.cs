using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;
using AppStore.Services.Abstract;

namespace AppStore.Services.Implementation
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository _libroRepository;

        public LibroService(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        public bool Add(Libro libro)
        {
            return _libroRepository.Add(libro);
        }

        public bool Delete(int id)
        {
            return _libroRepository.Delete(id);
        }

        public Libro GetById(int id)
        {
            return _libroRepository.GetById(id);
        }

        public LibroListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new LibroListVm();
            var list = _libroRepository.List().ToList();

            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.Titulo!.ToLower().StartsWith(term)).ToList();
            }

            if (paging)
            {
                int pageSize = 5;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }

            // Obtenemos las categorías y las unimos con los libros usando el repositorio
            var categorias = _libroRepository.GetCategorias();
            var libroCategorias = _libroRepository.GetLibroCategorias();

            foreach (var libro in list)
            {
                var nombresCategorias = (
                    from categoria in categorias
                    join lc in libroCategorias
                    on categoria.Id equals lc.CategoriaId
                    where lc.LibroId == libro.Id
                    select categoria.Nombre
                ).ToList();

                libro.CategoriasNames = string.Join(",", nombresCategorias);
            }

            data.LibroList = list.AsQueryable();
            return data;
        }

        public bool Update(Libro libro)
        {
            return _libroRepository.Update(libro);
        }

        public List<int> GetCategoriaByLibroId(int libroId)
        {
            return _libroRepository.GetCategoriaByLibroId(libroId);
        }
    }
}
