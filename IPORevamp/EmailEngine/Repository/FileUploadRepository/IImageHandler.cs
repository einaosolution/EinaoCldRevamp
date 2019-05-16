using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Drawing;
using EmailEngine.Repository.Interface;
using Microsoft.Extensions.Hosting.Internal;

namespace EmailEngine.Repository.FileUploadRepository
{
    public interface IFileHandler:IAutoDependencyRegister
    {
        Task<string> UploadFile(IFormFile FileDetail, string UploadPath, string FileAllowExtension, 
            int _oneMegaByte,int  _fileMaxSize);
        Image FetchImage(int? width, int? height, string fileName);
    }
 
    public enum FileType
    {
        PICTURE = 1,
        VIDEOS
    }
}

