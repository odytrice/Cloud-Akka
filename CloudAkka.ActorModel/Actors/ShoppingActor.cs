using Akka.Actor;
using CloudAkka.ActorModel.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudAkka.ActorModel.Actors
{
    public class ShoppingActor : ReceiveActor
    {
        Dictionary<string, IActorRef> _users = new Dictionary<string, IActorRef>();
        public ShoppingActor()
        {
            Receive<Login>(m => LoginUser(m));
        }

        public void LoginUser(Login message)
        {
            var userExists = _users.ContainsKey(message.UserName);
            if (!userExists && message.UserName != null)
            {
                var userProps = Props.Create<UserActor>(message.UserName);
                _users[message.UserName] = Context.ActorOf(userProps, message.UserName);
            }
        }
    }
}
