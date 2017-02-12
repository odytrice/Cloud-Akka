using Akka.Actor;
using CloudAkka.ActorModel.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudAkka.ActorModel.Actors
{
    public class EchoActor : ReceiveActor
    {
        public EchoActor()
        {
            ReceiveAsync<EchoMessage>(async m =>
            {
                //Simulate Computation
                await Task.Delay(200);

                Sender.Tell(new ResponseMessage(m.Message));
            });
        }
    }
}
