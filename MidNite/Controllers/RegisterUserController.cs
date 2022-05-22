using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MidNite.CommonHelpers;
using MidNite.Core.RegisterUsers;
using MidNite.EntityFramework.Data;
using MidNite.Service.RegisterUsers;
using System;
using System.Globalization;
using System.IO;

namespace MidNite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterUserController : ControllerBase
    {
        private readonly RegisterUsersService _registerUsersService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly MidNiteAPIDbContext _midNiteAPIDbContext;
        public RegisterUserController(RegisterUsersService RegisterUserService, MidNiteAPIDbContext midNiteAPIDbContext ,IWebHostEnvironment webHostEnvironment)
        {
            _registerUsersService = RegisterUserService;
            _hostEnvironment = webHostEnvironment;
            _midNiteAPIDbContext = midNiteAPIDbContext;
        }

        [HttpPost]
        public ActionResult RegisterUser()
        {
            RegisterUser registerUser = new RegisterUser();
            var FirstName = Request.Form["FirstName"].ToString();
            var LastName = Request.Form["LastName"].ToString();
            var UserName = Request.Form["UserName"].ToString();
            var Gender = Request.Form["Gender"].ToString();
            var Sexuailty = Request.Form["Sexuailty"].ToString();
            string DateOfBrith = Request.Form["DateOfBrith"].ToString();
            DateTime dateTime = DateTime.ParseExact(DateOfBrith, new string[] { "mmmm.dd.yyyy", "mm.dd.yyyy", "m.d.yyyy", "mm/dd/yyyy", "m/d/yyyy", "mmmm/dddd/yyyy", "mm-dd-yyyy", "m-d-yyyy", "mmmm-dddd-yyyy", },
                                       CultureInfo.InvariantCulture);
            var Password = Request.Form["Password"].ToString();
            var ProfileImage = Request.Form.Files;
            
            if (ProfileImage.Count > 0)
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

                if (!Directory.Exists(_hostEnvironment.WebRootPath + "\\uploads\\"))
                {
                    Directory.CreateDirectory(_hostEnvironment.WebRootPath + "\\uploads\\");
                }
                if (!Directory.Exists(_hostEnvironment.WebRootPath + "\\uploads\\" + "\\" + UserName + "\\"))
                {
                    Directory.CreateDirectory(_hostEnvironment.WebRootPath + "\\uploads\\" + "\\" + UserName + "\\");
                }
                string extension = Path.GetExtension(ProfileImage[0].FileName);
                bool CheckImageExtension = HelperFunction.GetImageExtension(extension.ToLower());

                if (CheckImageExtension)
                {
                    string DbPath;
                    using (FileStream filestream = System.IO.File.Create(_hostEnvironment.WebRootPath + "\\uploads\\" + "\\" + UserName + "\\" + ProfileImage[0].FileName))
                    {
                        ProfileImage[0].CopyTo(filestream);
                        DbPath = filestream.Name;
                        filestream.Flush();
                    }
                    const int workFactor = 10;
                    var HashedPassword = BCrypt.Net.BCrypt.HashPassword(Password, workFactor);
                    registerUser.DateOfBrith = dateTime;
                    registerUser.FirstName = FirstName;
                    registerUser.LastName = LastName;
                    registerUser.UserName = UserName;
                    registerUser.Gender = Gender;
                    registerUser.Sexuailty = Sexuailty;
                    registerUser.Sexuailty = Sexuailty;
                    registerUser.ImageProfilePath = DbPath;
                    registerUser.Password = HashedPassword;
                    _midNiteAPIDbContext.RegisterUsers.Add(registerUser);
                    _midNiteAPIDbContext.SaveChanges();
                    return Ok(registerUser);
                }
                else
                {
                    return BadRequest("selectimage");
                }
            }
            else
            {
                return BadRequest("imagenotselected");
            }
        }
    }
}
