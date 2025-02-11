using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppStore.Repositories.Implementation;

public class CategoriaService : ICategoriaService
{
	private readonly DataBaseContext _context;
	public CategoriaService(DataBaseContext context)
	{
		_context = context;
	}
	public IQueryable<Categoria> ListCategorias()
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

	public CategoriaListVm List(string term = "", bool paging = false, int currentPage = 0)
	{
		var data = new CategoriaListVm();
		var list = _context.Categorias.ToList();

		if (!string.IsNullOrEmpty(term))
		{
			term = term.ToLower();
			list = list.Where(a => a.Nombre!.StartsWith(term)).ToList();
		}

		if(paging)
		{
			data.PageSize = 5;
			data.CurrentPage = currentPage;
			data.TotalPages = (int)Math.Ceiling(list.Count / (double)data.PageSize);
			list = list.Skip((currentPage - 1) * data.PageSize).Take(data.PageSize).ToList();
		}

		foreach (var item in list)
		{
			item.CategoriasList = _context.Categorias.Select(x => new SelectListItem { Text = x.Nombre, Value = x.Id.ToString() }).ToList();
		}

		data.CategoriaList = list.AsQueryable();
		return data;
	}
}
