using EmailEngine.Repository.FileUploadRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
        public async Task<string> UploadFile(IFormFile FileDetail, string UploadPath,
            string expectedExtension, int _oneMegaByte, int  _fileMaxSize , IHostingEnvironment _hostingEnvironment)
        {
            try
            {
                string msg = "";
                if (FileDetail != null)
                {                    
                    var extension = FileDetail.FileName.Split('.')[1];
                    //"txt", "doc", "docx", "pdf", "xls", "xlsx"
                    //  var supportedTypes = new[] { expectedExtension };
                    var supportedTypes = expectedExtension.Split(',');
                   int checkExtension = 0;
                    foreach(var item in supportedTypes)
                    {

                       var  kk = item.Replace("\"", "");
                        if (kk == extension)
                        
                        {
                            checkExtension = checkExtension + 1;
                        }
                    }

                    var filesize = _fileMaxSize * _oneMegaByte;

                    if (checkExtension == 0)

                    {
                        msg = "FAIL|File Extension Is InValid - Only Upload " + expectedExtension;
                    
                    }
                    
                    else if (FileDetail.Length ==0)
                    {
                        msg = "FAIL|File contain no content!";
                       
                    }

                    

                    else if (FileDetail.Length > filesize)
                    {
                        msg =  "FAIL|File size is larger than the accepted maximum size. Maximum file size is " + _fileMaxSize + " MB!";
                   
                    }
                    
                    
                   var fileName = Guid.NewGuid ().ToString();
                    fileName += "."+extension;

                    string folderName = UploadPath;
                   
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(webRootPath, folderName);

                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        FileDetail.CopyTo(stream);
                    }


                   // var fileDirectory = $"{UploadPath.ToString().ToLower()}";                                   

                  //  var newPath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory, fileName);
                  //  using (var fs = new FileStream(newPath, FileMode.Create))
                  //  {
                   //    await FileDetail.CopyToAsync(fs);
                   // }

                    msg= "OK|"+ fileName;

                    return msg;
                }

                return null;
            }
            catch(Exception ex)
            {
                var ec = ex;
                return null;
            }           
        }
    }
}
