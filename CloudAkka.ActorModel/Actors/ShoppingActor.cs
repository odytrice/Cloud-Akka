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
            Receive<AddProduct>(m =>
            {
                //Route Message to specific Child Actor
                var userActor = _users.Where(u => u.Key == m.User).Select(u => u.Value).FirstOrDefault();
                if(userActor != null)
                {
                    userActor.Forward(m);
                }
            });
        }

        public void LoginUser(Login message)
        {
            if (string.IsNullOrEmpty(message.UserName)) return;

            var userExists = _users.ContainsKey(message.UserName);
            if (!userExists)
            {
                //Create User Actor
                var userProps = Props.Create<UserActor>(message.UserName);
                _users[message.UserName] = Context.ActorOf(userProps, message.UserName);
            }

            //Forward Login Message
            _users[message.UserName].Forward(message);
        }
    }
}
