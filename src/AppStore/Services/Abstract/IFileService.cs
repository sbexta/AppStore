namespace AppStore.Services.Abstract;

public interface IFileService
{
    public Tuple<int, string> SaveImage(IFormFile path);
    public bool DeleteImage(string path);
}