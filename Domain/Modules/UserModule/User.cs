using Domain.Modules.BookingsModule;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules.UserModule
{
    public class User:IdentityUser<int>
    {
      
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
       

    }
}
