using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MidNite.CommonHelpers;
using MidNite.Core.Events;
using MidNite.EntityFramework.Data;
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
    [ApiController]
    
    public class EventController : ControllerBase
    {
       
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly MidNiteAPIDbContext _midNiteAPIDbContext;

        public EventController(IWebHostEnvironment webHostEnvironment, MidNiteAPIDbContext midNiteAPIDbContext)
        {
            _hostEnvironment = webHostEnvironment;
            _midNiteAPIDbContext = midNiteAPIDbContext;
        }


        [HttpGet]
        [Route("api/Event")]
        public IActionResult Event()
        {
            var eventObj = new Event();

            eventObj.Title = Request.Form["Title"];
            int CurrentUserId = int.Parse(Request.Form["CurrentUserId"]);
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
            string DbPath = string.Empty;

            
            if (EventPicture.Count > 0)
            {
                int userId = _midNiteAPIDbContext.RegisterUsers.Where(x => x.RegisterUserId == CurrentUserId)
                    .Select(x => x.RegisterUserId)
                    .FirstOrDefault();

                if (userId !=0)
                {
                    DbPath = HelperFunction.SetPathForEvent(_hostEnvironment, EventPicture[0], userId, DbPath);
                }
            }
            else {
                return BadRequest("provideimage");
            }

            if (DbPath != "selectimage" && DbPath != "")
            {
                eventObj.EventPicturePath = DbPath;
                _midNiteAPIDbContext.Events.Add(eventObj);
                _midNiteAPIDbContext.SaveChanges();
                return Ok(eventObj);
            }
            else
            {
                eventObj.EventPicturePath = string.Empty ;
                _midNiteAPIDbContext.Events.Add(eventObj);
                _midNiteAPIDbContext.SaveChanges();
                return Ok(eventObj);
            }

        }

        [HttpGet]
        [Route("api/Event/SearchEvent/{tile}")]
        public List<SearchEventDto> SearchEvent(string tile)
        {
            var EventList = _midNiteAPIDbContext.Events.Where(x => tile.Contains(x.Title))
                .Select(x => new SearchEventDto
                {
                    Title = x.Title
                }).ToList();

            return EventList;
        }

        [HttpPost]
        [Route("api/Event/ShowAllEvent")]
        public IActionResult ShowAllEvent()
        {
            var EventList = _midNiteAPIDbContext.Events
                .Select(x => new SearchEventDto
                {
                    Title = x.Title
                }).ToList();

            if (EventList.Count == 0)
            {
                return Ok("Events not available");
            }
            else
            {
                return Ok(EventList);
            }
        }

        [HttpPost]
        [Route("api/Event/Fivepicture")]
        public ActionResult SendFivePicture()
        {
            bool PathExit = false;
            string DirPath = string.Empty;
            string PathFor5Pic = string.Empty;
            var PictureList = Request.Form.Files;
            //int CurrentUserId = int.Parse(Request.Form["CurrentUserId"]);
            int CurrentUserId = 1;
            
            if (PictureList.Count > 0)
            {
                var UserNameAndId = _midNiteAPIDbContext.RegisterUsers.Where(x => x.RegisterUserId == CurrentUserId)
                    .Select(x => new
                    {
                        x.UserName,
                        x.RegisterUserId
                    }).FirstOrDefault();


                for (int i = 0; i < 5; i++)
                {
                    if (!PathExit)
                    {
                        (DirPath, PathExit) = HelperFunction.SetPathForEventForPIcture(_hostEnvironment, PathExit, DirPath);
                        if (PathExit)
                        {
                            string WithUserNamePath = DirPath + "\\" + UserNameAndId.UserName.ToString() + "\\";
                            string CompletePath = WithUserNamePath + "\\" + UserNameAndId.RegisterUserId.ToString() + "\\";

                            if (!Directory.Exists(CompletePath))
                            {
                                Directory.CreateDirectory(CompletePath);
                            }
                            PathFor5Pic = Path.GetDirectoryName(CompletePath);
                        }
                    }
                    if (PathFor5Pic != "")
                    {
                        if (HelperFunction.GetImageExtension(Path.GetExtension(PictureList[i].FileName).ToLower()))
                        {
                            using (FileStream filestream = System.IO.File.Create(PathFor5Pic +"\\"+ PictureList[i].FileName))
                            {
                                PictureList[i].CopyTo(filestream);
                                //DbPath = filestream.Name;
                                filestream.Flush();
                            }
                        }
                    }
                }


                #region Commmented

               /* foreach (var pic in PictureList)
                {

                    if (count > 4)
                    {
                        break;
                    }
                    if (!PathExit)
                    {
                        (Path, PathExit) = HelperFunction.SetPathForEventForPIcture(_hostEnvironment, PathExit, Path);
                        if (PathExit)
                        {
                            string WithUserNamePath = Path + "\\" + (UserNameAndId[0]).ToString();
                            string CompletePath = WithUserNamePath + "\\" + (UserNameAndId[1]).ToString();

                            if (!Directory.Exists(CompletePath))
                            {
                                Directory.CreateDirectory(CompletePath);
                                PathFor5Pic = System.IO.Path.GetDirectoryName(CompletePath);
                            }
                        }
                    }
                    if (PathFor5Pic != "")
                    {
                        if (HelperFunction.GetImageExtension(System.IO.Path.GetExtension(pic.FileName).ToLower()))
                        {
                            using (FileStream filestream = System.IO.File.Create(PathFor5Pic + pic.FileName))
                            {
                                pic.CopyTo(filestream);
                                //DbPath = filestream.Name;
                                filestream.Flush();
                            }
                        }
                    }
                    count += 1;
                }*/
                #endregion

            }

            return Ok();
        }




        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

    }
}
