using Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class EmailSettings
    {
        public static async Task<bool> SendEmail(Email email)
        {
            try
            {
                using var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("diaaemad156@gmail.com", "hpgcxzmdgmlooqnv");

               
                await client.SendMailAsync("diaaemad156@gmail.com", email.To, email.Subject, email.Body);
                return true;
            }
            catch (Exception)
            {
                return false; 
            }
        }
    }
}
