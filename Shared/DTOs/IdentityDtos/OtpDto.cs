using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.IdentityDtos
{
    public class OtpDto
    {
        public string Email { get; set; }=default!;
        public string Code { get; set; }= default!;
    }
}
