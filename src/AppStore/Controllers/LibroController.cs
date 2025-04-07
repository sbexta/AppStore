using Microsoft.AspNetCore.Mvc;
using AppStore.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppStore.Services.Abstract;

namespace AppStore.Controllers;

public class LibroController : Controller
{
    private readonly ILibroService _libroService;
    private readonly ICategoriaService _categoriaService;
    private readonly IFileService _fileService;
    public LibroController(ILibroService libroService,
                           ICategoriaService categoriaService,
                           IFileService fileService)
    {
        _libroService = libroService;
        _categoriaService = categoriaService;
        _fileService = fileService;
    }




    public IActionResult ListCategoria()
    {
        var result = _categoriaService.ListCategorias().Select(a => new SelectListItem { Text = a.Nombre, Value = a.Id.ToString() }).ToList();
        return View(result);
    }

    public IActionResult Add()
    {
        var libro = new Libro();
        libro.CategoriasList = _categoriaService.ListCategorias().Select(a => new SelectListItem { Text = a.Nombre, Value = a.Id.ToString() }).ToList();
        return View(libro);
    }

    [HttpPost]
    public IActionResult Add(Libro libro)
    {
        libro.CategoriasList = _categoriaService.ListCategorias()
                .Select(a => new SelectListItem { Text = a.Nombre, Value = a.Id.ToString() });
        if (!ModelState.IsValid)
        {
            return View(libro);
        }

        if (libro.ImageFile != null)
        {
            var resultado = _fileService.SaveImage(libro.ImageFile);
            if (resultado.Item1 == 0)
            {
                TempData["msg"] = "La imagen no pudo guardarse exitosamente";
                return View(libro);
            }

            var imagenName = resultado.Item2;
            libro.Imagen = imagenName;

        }

        var resultadoLibro = _libroService.Add(libro);
        if (resultadoLibro)
        {
            TempData["msg"] = "Se agrego el libro exitosamente";
            return RedirectToAction(nameof(Add));
        }

        TempData["msg"] = "Errores guardando el libro";
        return View(libro);
    }

    public IActionResult Edit(int id)
    {
        var libro = _libroService.GetById(id);
        var categoriaDeLibro = _libroService.GetCategoriaByLibroId(id);
        var categoria = new MultiSelectList(_categoriaService.ListCategorias(), "Id", "Nombre", categoriaDeLibro);

        libro.CategoriasList = categoria;
        return View(libro);
    }

    [HttpPost]
    public IActionResult Edit(Libro model)
    {
        var categoriaDeLibro = _libroService.GetCategoriaByLibroId(model.Id);
        var categoria = new MultiSelectList(_categoriaService.ListCategorias(), "Id", "Nombre", categoriaDeLibro);

        model.CategoriasList = categoria;

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.ImageFile != null)
        {
            var result = _fileService.SaveImage(model.ImageFile);
            if (result.Item1 == 0)
            {
                TempData["msg"] = "La imagen no pudo guardarse exitosamente";
                return View(model);
            }

            var imagenName = result.Item2;
            model.Imagen = imagenName;

        }
        
        var resultado = _libroService.Update(model);
        if (!resultado)
        {
            TempData["msg"] = "Error al actualizar el libro";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }

        TempData["msg"] = "Se actualizo el libro exitosamente";
        return View(model);
    }

    public IActionResult LibroList(int id)
    {
        var libros = _libroService.List();
        return View(libros);
    }

    public IActionResult Delete(int id)
    {
        try
        {
            _libroService.Delete(id);
            TempData["msg"] = "Se elimino el libro exitosamente";
            return RedirectToAction(nameof(LibroList));
        }
        catch (Exception)
        {
            TempData["msg"] = "Error al eliminar el libro";
            return RedirectToAction(nameof(LibroList));
        }
    }
}
        