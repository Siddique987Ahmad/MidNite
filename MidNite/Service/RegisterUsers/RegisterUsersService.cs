using MidNite.EntityFramework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidNite.Service.RegisterUsers
{
    public class RegisterUsersService
    {
        private readonly MidNiteAPIDbContext _midNiteAPIDbContext;
        public RegisterUsersService(MidNiteAPIDbContext midNiteAPIDbContext)
        {
            _midNiteAPIDbContext = midNiteAPIDbContext;
        }

        public async Task CreateUserAsync()
        {
            
            //await _midNiteAPIDbContext.RegisterUsers.AddAsync(CreateUser);
            //await _midNiteAPIDbContext.SaveChangesAsync();
        }
    }
    
}
