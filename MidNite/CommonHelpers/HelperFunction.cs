using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MidNite.CommonHelpers
{
    public class HelperFunction
    {
        public static bool GetImageExtension(string ex)
        {
            bool CheckExtension = false;
            string[] extension = { ".png", ".jpg", ".jpeg", ".jfif" };
            if (extension.Contains(ex))
            {
                CheckExtension = true;
            }
            return CheckExtension;
        }

        public static string SetPathForEvent(IWebHostEnvironment webHostEnvironment, IFormFile formFile, int userId, string DbPath)
        {
            string path = Directory.GetCurrentDirectory();
            if (path.Equals(Directory.GetCurrentDirectory()))
            {

                string path2 = Path.Combine(path, "wwwroot");
                if (!Directory.Exists(path2))
                {
                    Directory.CreateDirectory(path2);
                }
            }

            if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\Events\\"))
            {
                Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\Events\\");
            }
            if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\Events\\"  + userId.ToString() + "\\"))
            {
                Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\Events\\" + userId.ToString() + "\\");
            }
           
            string extension = Path.GetExtension(formFile.FileName);
            bool CheckImageExtension = GetImageExtension(extension.ToLower());

            if (CheckImageExtension)
            {
                
                using (FileStream filestream = File.Create(webHostEnvironment.WebRootPath + "\\Events\\"+ userId + "\\" + formFile.FileName))
                {
                    formFile.CopyTo(filestream);
                    DbPath = filestream.Name;
                    filestream.Flush();
                }

                return DbPath;

            }
            else
            {
                return "selectimage";
            }
        }




        internal static string SetPathUserProfile(IWebHostEnvironment hostEnvironment, string UserName, IFormFile formFile, string DbPath)
        {
            string path = Directory.GetCurrentDirectory();
            if (path.Equals(Directory.GetCurrentDirectory()))
            {

                string path2 = Path.Combine(path, "wwwroot");
                if (!Directory.Exists(path2))
                {
                    Directory.CreateDirectory(path2);
                }
            }

            if (!Directory.Exists(hostEnvironment.WebRootPath + "\\uploads\\"))
            {
                Directory.CreateDirectory(hostEnvironment.WebRootPath + "\\uploads\\");
            }
            if (!Directory.Exists(hostEnvironment.WebRootPath + "\\uploads\\" + UserName + "\\"))
            {
                Directory.CreateDirectory(hostEnvironment.WebRootPath + "\\uploads\\" + UserName + "\\");
            }
            string extension = Path.GetExtension(formFile.FileName);
            bool CheckImageExtension = GetImageExtension(extension.ToLower());

            if (CheckImageExtension)
            {

                using (FileStream filestream = File.Create(hostEnvironment.WebRootPath + "\\uploads\\" + "\\" + UserName + "\\" + formFile.FileName))
                {
                    formFile.CopyTo(filestream);
                    DbPath = filestream.Name;
                    filestream.Flush();
                }
                return DbPath;
            }
            else
            {
                return "selectimage";
            }
        }

        public static (string, bool) SetPathForEventForPIcture(IWebHostEnvironment hostEnvironment, bool pathExit, string path)
        {
            
            if (Directory.GetCurrentDirectory().Equals(Directory.GetCurrentDirectory()))
            {

                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
                }
            }

            if (!Directory.Exists(hostEnvironment.WebRootPath + "\\FriendPic\\"))
            {
                string strPath = hostEnvironment.WebRootPath + "\\FriendPic\\";
                Directory.CreateDirectory(strPath);
                path = Path.GetDirectoryName(strPath);
                pathExit = true;
            }
            return (path, pathExit);
        }
    }
}
