using AppStore.Repositories.Abstract;
using AppStore.Services.Abstract;
using Microsoft.AspNetCore.Http;

namespace AppStore.Services.Implementation
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public Tuple<int, string> SaveImage(IFormFile file)
        {
            return _fileRepository.SaveImage(file);
        }

        public bool DeleteImage(string fileName)
        {
            return _fileRepository.DeleteImage(fileName);
        }
    }
}
