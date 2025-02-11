using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public Tuple<int, string> SaveImage(IFormFile path)
    {
        try
        {
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var uploadsDirectory = Path.Combine(wwwRootPath, "Uploads");

            // Crear el directorio si no existe
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            var extension = Path.GetExtension(path.FileName);
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            // Verificar las extensiones permitidas
            if (!allowedExtensions.Contains(extension.ToLower()))
            {
                return new Tuple<int, string>(0, "Solo se permiten archivos jpg, jpeg y png");
            }

            // Generar un nombre único para el archivo
            var fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsDirectory, fileName);

            // Usar 'using' para asegurar que el Stream se cierra apropiadamente
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                path.CopyTo(stream);
            }

            return new Tuple<int, string>(1, fileName);
        }
        catch (Exception ex)
        {
            // Puedes registrar el mensaje de error o lanzarlo dependiendo de la estrategia de manejo de errores
            return new Tuple<int, string>(0, $"Error al capturar la imagen: {ex.Message}");
        }
    }


    public bool DeleteImage(string path)
    {
        try
        {
            var wwwRoooPath = _webHostEnvironment.WebRootPath;
            var file = Path.Combine(wwwRoooPath, "Uploads\\", path);
            if (File.Exists(file))
            {
                File.Delete(file);
                return true;
            }
            return false;
            
        }catch(Exception)
        {
            return false;
        }
        
    }
}