using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MidNite.CommonHelpers;
using MidNite.Core.RegisterUsers;
using MidNite.EntityFramework.Data;
using MidNite.Service.RegisterUsers;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MidNite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterUserController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly MidNiteAPIDbContext _midNiteAPIDbContext;
        public RegisterUserController(MidNiteAPIDbContext midNiteAPIDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _hostEnvironment = webHostEnvironment;
            _midNiteAPIDbContext = midNiteAPIDbContext;
        }

        [HttpPost]
        public ActionResult RegisterUser()
        {
            RegisterUser registerUser = new();
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
            string DbPath = string.Empty;
            const int workFactor = 10;
            if (ProfileImage.Count > 0)
            {
                DbPath = HelperFunction.SetPathUserProfile(_hostEnvironment, UserName, ProfileImage[0], DbPath);
            }
            else
            {
                return BadRequest("imagenotselected");
            }
            if (DbPath != "" || DbPath != "selectimage")
            {
                
                var HashedPassword = BCrypt.Net.BCrypt.HashPassword(Password, workFactor);
                registerUser.DateOfBrith = dateTime;
                registerUser.FirstName = FirstName;
                registerUser.LastName = LastName;
                //registerUser.UserName = UserName;
                registerUser.Gender = Gender;
                registerUser.Sexuailty = Sexuailty;
                registerUser.Sexuailty = Sexuailty;
                registerUser.ImageProfilePath = DbPath;
                registerUser.Password = HashedPassword;
                var uniqueUserName = _midNiteAPIDbContext.RegisterUsers.Where(x => x.UserName.ToLower() == UserName.ToLower())
                    .Select(x => x.UserName)
                    .FirstOrDefault();
                if (uniqueUserName is not null)
                {
                    registerUser.UserName = UserName;
                    _midNiteAPIDbContext.RegisterUsers.Add(registerUser); // add check for with unique index 'IX_RegisterUsers_UserName
                    _midNiteAPIDbContext.SaveChanges();
                    return Ok(registerUser);
                }
                else
                {
                    return Ok("Error"); // User already Exit
                }
            }
            else
            {
                var HashedPassword = BCrypt.Net.BCrypt.HashPassword(Password, workFactor);
                registerUser.DateOfBrith = dateTime;
                registerUser.FirstName = FirstName;
                registerUser.LastName = LastName;
                //registerUser.UserName = UserName;
                registerUser.Gender = Gender;
                registerUser.Sexuailty = Sexuailty;
                registerUser.Sexuailty = Sexuailty;
                registerUser.ImageProfilePath = DbPath;
                registerUser.Password = HashedPassword;
                var uniqueUserName = _midNiteAPIDbContext.RegisterUsers.Where(x => x.UserName.ToLower() == UserName.ToLower())
                    .Select(x => x.UserName)
                    .FirstOrDefault();
                if (uniqueUserName is not null)
                {
                    registerUser.UserName = UserName;
                    _midNiteAPIDbContext.RegisterUsers.Add(registerUser); // add check for with unique index 'IX_RegisterUsers_UserName
                    _midNiteAPIDbContext.SaveChanges();
                    return Ok(registerUser);
                }
                else
                {
                    return Ok("Error"); // User already Exit
                }
               
            }
        }
    }
}
