

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MidNite.CommonHelpers;
using MidNite.Service.RegisterUsers;
using System.IO;

namespace MidNite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterUserController : ControllerBase
    {
        private readonly RegisterUsersService _registerUsersService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public RegisterUserController(RegisterUsersService RegisterUserService, IWebHostEnvironment webHostEnvironment)
        {
            _registerUsersService = RegisterUserService;
            _hostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public ActionResult RegisterUser()
        {
            var FirstName = Request.Form["FirstName"].ToString();
            var LastName = Request.Form["LastName"].ToString();
            var UserName = Request.Form["UserName"].ToString();
            var Gender = Request.Form["Gender"].ToString();
            var Sexuailty = Request.Form["Sexuailty"].ToString();
            var DateOfBrith = Request.Form["DateOfBrith"].ToString();
            var Password = Request.Form["Password"].ToString();
            var ProfileImage = Request.Form.Files;
            //_registerUsersService.CreateUserAsync();
            /// cuhange

            if (ProfileImage.Count > 0)
            {
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
                    using (FileStream filestream = System.IO.File.Create(_hostEnvironment.WebRootPath + "\\uploads\\" + "\\" + UserName + "\\" + ProfileImage[0].FileName))
                    {
                        ProfileImage[0].CopyTo(filestream);
                        filestream.Flush();
                    }
                    return Ok();
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
