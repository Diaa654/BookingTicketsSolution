using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.IdentityDtos
{
    public class ConfirmEmailDto
    {
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
