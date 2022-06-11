using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MidNite.Core.RegisterUsers
{
    [Index(nameof(UserName), IsUnique = true)]
    public class RegisterUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegisterUserId { get; set; }
        [MaxLength(25)]
        public string FirstName { get; set; }
        [MaxLength(25)]
        public string LastName { get; set; }
        [MaxLength(25)]
        public string UserName { get; set; }
        
        public DateTime DateOfBrith { get; set; }
        public string Gender { get; set; }
        public string Sexuailty { get; set; }
        public string ImageProfilePath { get; set; }
        [MinLength(8)]
        public string Password { get; set; }

        public int LoginUserId { get; set; }
    }
}
