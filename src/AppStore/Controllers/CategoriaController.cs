using AppStore.Models.Domain;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppStore.Controllers;

public class CategoriaController : Controller
{
    private readonly ICategoriaService _categoriaService;
    private readonly ILibroService _libroService;
	public CategoriaController(ICategoriaService categoriaService, 
                               ILibroService libroService)
	{
		_categoriaService = categoriaService;
		_libroService = libroService;
	}

	public IActionResult Add()
    {
        var categoria = new Categoria();
		categoria.CategoriasList = _categoriaService.ListCategorias().Select(x => new SelectListItem { Text = x.Nombre, Value = x.Id.ToString()}).ToList();
        return View(categoria);
    }

    [HttpPost]
    public IActionResult Add(Categoria categoria)
    {
        if (ModelState.IsValid)
        {
			categoria.CategoriasList = _categoriaService.ListCategorias().Select(x => new SelectListItem { Text = x.Nombre, Value = x.Id.ToString() }).ToList();


            if (categoria.Nombre != null)
            {
                var result = _categoriaService.Add(categoria);
                if (result)
                {
                    TempData["msg"] = "Se agrego la categoria";
                    return RedirectToAction("Add", "Categoria");
                }
            }
		}

		TempData["msg"] = "Errores guardando la categoria";
		return View();
    }

    public IActionResult CategoriaList()
    {
        var data = _categoriaService.List();
        return View(data);
    }
}

