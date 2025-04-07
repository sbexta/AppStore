using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Interfaz;
using AppStore.Services.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppStore.Services.Implementation
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IQueryable<Categoria> ListCategorias()
        {
            return _categoriaRepository.List();
        }

        public bool Add(Categoria categoria)
        {
            return _categoriaRepository.Add(categoria);
        }

        public CategoriaListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new CategoriaListVm();
            var list = _categoriaRepository.List().ToList();

            if (!string.IsNullOrEmpty(term))
            {
                list = _categoriaRepository.Search(term).ToList();
            }

            if (paging)
            {
                data.PageSize = 5;
                data.CurrentPage = currentPage;
                data.TotalPages = (int)Math.Ceiling(list.Count / (double)data.PageSize);
                list = list.Skip((currentPage - 1) * data.PageSize).Take(data.PageSize).ToList();
            }

            foreach (var item in list)
            {
                item.CategoriasList = _categoriaRepository.List()
                    .Select(x => new SelectListItem { Text = x.Nombre, Value = x.Id.ToString() })
                    .ToList();
            }

            data.CategoriaList = list.AsQueryable();
            return data;
        }
    }
}
