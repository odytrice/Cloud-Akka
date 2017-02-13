using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using CloudAkka.ActorModel.Models;

namespace CloudAkka.ActorModel.Messages.Commands
{
    public class FetchCartStatus
    {
        public string User { get; }

        public FetchCartStatus(string user)
        {
            User = user;
        }
    }
}
