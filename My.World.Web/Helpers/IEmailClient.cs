using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.Helpers
{
    public interface IEmailClient
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
