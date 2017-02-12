using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudAkka.ActorModel.Messages.Commands
{
    public class Login
    {
        public string UserName { get; }

        public Login(string userName)
        {
            UserName = userName;
        }
    }
}
