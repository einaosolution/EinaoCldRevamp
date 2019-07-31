using EmailEngine.Repository.FileUploadRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;


namespace EmailEngine.Repository
{
    public class FileHandler:IFileHandler
    {
     
        public Image FetchImage(int? width, int? height, string fileName)
        {
            try
            {
                var fileDirectory = $"{FileType.PICTURE.ToString().ToLower()}s";

                var newPath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory, fileName);
                var img = Image.FromFile(newPath);

                if (width.HasValue && height.HasValue)
                {
                    return new Bitmap(img, new Size(width.Value, height.Value));
                }

                return img;
            }
            catch(Exception)
            {
                return null;
            }
            
        }
        //public async Task<string> UploadFile1(IFormFile FileDetail, string UploadPath,
        //    string expectedExtension, int _oneMegaByte, int  _fileMaxSize , Microsoft.AspNetCore.Hosting.HostingEnvironment _hostingEnvironment)
        //{
               
        //}


        public async Task<string> UploadFile(IFormFile FileDetail, string UploadPath, string FileAllowExtension, 
                                int _oneMegaByte, int _fileMaxSize)
        {
            try
            {
                string msg = "";
                if (FileDetail != null)
                {
                    var extension = FileDetail.FileName.Split('.')[1];
                    //"txt", "doc", "docx", "pdf", "xls", "xlsx"
                    //  var supportedTypes = new[] { expectedExtension };
                    var supportedTypes = FileAllowExtension.Split(',');
                    int checkExtension = 0;
                    foreach (var item in supportedTypes)
                    {

                        var kk = item.Replace("\"", "");
                        if (kk == extension)

                        {
                            checkExtension = checkExtension + 1;
                        }
                    }

                    var filesize = _fileMaxSize * _oneMegaByte;

                    if (checkExtension == 0)

                    {
                        msg = "FAIL|File Extension Is InValid - Only Upload " + FileAllowExtension;

                    }

                    else if (FileDetail.Length == 0)
                    {
                        msg = "FAIL|File contain no content!";

                    }



                    else if (FileDetail.Length > filesize)
                    {
                        msg = "FAIL|File size is larger than the accepted maximum size. Maximum file size is " + _fileMaxSize + " MB!";

                    }


                    var fileName = Guid.NewGuid().ToString();
                    fileName += "." + extension;

                   
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), UploadPath, fileName);                 
                    using (var fileSrteam = new FileStream(fullPath, FileMode.Create))
                    {
                        await FileDetail.CopyToAsync(fileSrteam);
                    }

                   

                    msg = fileName;

                    return msg;
                }

                return "FAIL| No File Uploaded";
            }
            catch (Exception ex)
            {
                var ec = ex;
                return "FAIL|" + ex.Message;
            }
        }
    }
}
