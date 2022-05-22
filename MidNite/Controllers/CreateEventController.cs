using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MidNite.CommonHelpers;
using MidNite.Core.Events;
using MidNite.Service.Events.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MidNite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateEventController : ControllerBase
    {
        // GET: api/<CreateEventController>
        private readonly IWebHostEnvironment _hostEnvironment;

        public CreateEventController(IWebHostEnvironment webHostEnvironment)
        {
            _hostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        public IActionResult Get(CreateEventDto input)
        {
            var eventObj = new Event();

            eventObj.Title = Request.Form["Title"];
            eventObj.Information = Request.Form["Information"];
            string StartTime = Request.Form["StartTime"];
            DateTime StartDateTime = DateTime.ParseExact(StartTime, new string[] { "mmmm.dd.yyyy", "mm.dd.yyyy", "m.d.yyyy", "mm/dd/yyyy", "m/d/yyyy", "mmmm/dddd/yyyy", "mm-dd-yyyy", "m-d-yyyy", "mmmm-dddd-yyyy", },
                                      CultureInfo.InvariantCulture);
            eventObj.StartTime = StartDateTime;
            string EndTime = Request.Form["EndTime"].ToString();

            DateTime EndDateTime = DateTime.ParseExact(EndTime, new string[] { "mmmm.dd.yyyy", "mm.dd.yyyy", "m.d.yyyy", "mm/dd/yyyy", "m/d/yyyy", "mmmm/dddd/yyyy", "mm-dd-yyyy", "m-d-yyyy", "mmmm-dddd-yyyy", },
                                      CultureInfo.InvariantCulture);
            eventObj.StartTime = EndDateTime;

            eventObj.Host = Request.Form["Host"].ToString();
            eventObj.City = Request.Form["City"].ToString();
            eventObj.AmountOfGuest = int.Parse(Request.Form["AmountOfGuest"]);
            eventObj.AverageAge = float.Parse(Request.Form["AverageAge"]);
            eventObj.MaleFemaleRatio = Request.Form["MaleFemaleRatio"].ToString();

            var EventPicture = Request.Form.Files;
            
            // Add Unique Name for path


            if (EventPicture.Count > 0)
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

                if (!Directory.Exists(_hostEnvironment.WebRootPath + "\\Events\\"))
                {
                    Directory.CreateDirectory(_hostEnvironment.WebRootPath + "\\Events\\");
                }
                if (!Directory.Exists(_hostEnvironment.WebRootPath + "\\Events\\" + "\\" + "UserName" + "\\"))
                {
                    Directory.CreateDirectory(_hostEnvironment.WebRootPath + "\\Events\\" + "\\" + "UserName" + "\\");
                }
                string extension = Path.GetExtension(EventPicture[0].FileName);
                bool CheckImageExtension = HelperFunction.GetImageExtension(extension.ToLower());

                if (CheckImageExtension)
                {
                    string DbPath;
                    using (FileStream filestream = System.IO.File.Create(_hostEnvironment.WebRootPath + "\\Events\\" + "\\" + "UserName" + "\\" + EventPicture[0].FileName))
                    {
                        EventPicture[0].CopyTo(filestream);
                        DbPath = filestream.Name;
                        filestream.Flush();
                    }

                    eventObj.EventPicturePath = DbPath;
                   
                }
                else
                {
                    return BadRequest("selectimage");
                }
            }


            return Ok();
        }

        // GET api/<CreateEventController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CreateEventController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CreateEventController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CreateEventController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
