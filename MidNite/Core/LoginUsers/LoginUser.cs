using MidNite.Core.RegisterUsers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MidNite.Core.LoginUsers
{
    public class LoginUser
    {
        public int LoginUserId { get; set; }

        public List<RegisterUser> RegisterUser { get; set; }
    }
}
