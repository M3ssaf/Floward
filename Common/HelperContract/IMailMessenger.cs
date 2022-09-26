using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.HelperContract
{
    public interface IMailMessenger
    {
        Task SendMail(string targetMail, string Title, string Message);
    }
}
