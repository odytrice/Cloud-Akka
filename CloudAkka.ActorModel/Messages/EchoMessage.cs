using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudAkka.ActorModel.Messages
{
    public class EchoMessage
    {
        public string Message { get; }

        public EchoMessage(string message)
        {
            Message = message;
        }
    }
}
