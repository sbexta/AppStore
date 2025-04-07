using AppStore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace src.AppStore.Controllers;
public class HomeController : Controller
{
    private readonly ILibroService? _libroService;
    private readonly ICategoriaService? _categoriaService;
    public HomeController(ILibroService libroService,
                          ICategoriaService categoriaService)
    {
        _libroService = libroService;
        _categoriaService = categoriaService;
    }


    public IActionResult Index(string term ="" , int CurrentPage = 1)
    {
        var result = _libroService!.List(term, true, CurrentPage);
        return View(result);
    }

    public IActionResult LibroDetail(int libroId)
    {
        var result = _libroService!.GetById(libroId);
        return View(result);
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Categoria()
    {
        return _categoriaService!.ListCategorias().ToList().Count == 0 ? View("Empty") : View(_categoriaService!.ListCategorias());
    }
}
