using Microsoft.AspNetCore.Mvc;
using MidNite.EntityFramework.Data;
using MidNite.Service.LoginUsers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MidNite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUserController : ControllerBase
    {
        // GET: api/<LoginUserController>
        private readonly MidNiteAPIDbContext _midNiteAPIDbContext;

        public LoginUserController(MidNiteAPIDbContext midNiteAPIDbContext)
        {
            _midNiteAPIDbContext = midNiteAPIDbContext;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginUserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoginUserController>
        [HttpPost]
        public ActionResult Post(LoginUserDto input)
        {
            
            var getHashedPassword = _midNiteAPIDbContext.RegisterUsers.Where(b => b.UserName == input.UserName).Select(a => a.Password).FirstOrDefault();
            if (getHashedPassword != null)
            {
                if (BCrypt.Net.BCrypt.Verify(input.Password, getHashedPassword))
                {
                    return Ok("Successful");
                }
                else
                {
                    return BadRequest("Password Not match");
                }
            }
            else
            {
                return BadRequest("User Not Exit");
            }
            //_midNiteAPIDbContext.RegisterUsers.Where(a => a.UserName == value && (BCrypt.Net.BCrypt.Verify(value,)).GetHashCode();
        }

        // PUT api/<LoginUserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
