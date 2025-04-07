using Microsoft.AspNetCore.Http;

namespace AppStore.Repositories.Abstract
{
    public interface IFileRepository
    {
        Tuple<int, string> SaveImage(IFormFile file);
        bool DeleteImage(string fileName);
    }
}
