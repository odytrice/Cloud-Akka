using Akka.Actor;
using CloudAkka.ActorModel.External;
using CloudAkka.ActorModel.Messages.Commands;
using CloudAkka.ActorModel.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudAkka.ActorModel.Actors
{
    public class SignalRActor: ReceiveActor
    {
        private IEventPusher _eventPusher;
        private IActorRef _shoppingActor;

        public SignalRActor(IEventPusher eventPusher, IActorRef shoppingActor)
        {
            _eventPusher = eventPusher;
            _shoppingActor = shoppingActor;

            Receive<AddProduct>(m => shoppingActor.Tell(m));
            Receive<Login>(m => shoppingActor.Tell(m));

            Receive<CartStatus>(m =>
            {
                _eventPusher.OnCartLoaded(m.User, m.Items);
            });

            Receive<ProductAdded>(m =>
            {
                _eventPusher.OnProductAdded(m.User, m.Product);
            });
        }
    }
}
