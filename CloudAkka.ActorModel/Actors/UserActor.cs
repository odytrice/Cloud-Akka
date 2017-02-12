using Akka.Actor;
using CloudAkka.ActorModel.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudAkka.ActorModel.Actors
{
    public class UserActor: ReceiveActor
    {
        private string _userName;

        private IActorRef _cart;

        public UserActor(string userName)
        {
            _userName = userName;

            //Create Shopping Cart
            _cart = Context.ActorOf<CartActor>();

            //Forward AddProduct Message to Cart Actor
            Receive<AddProduct>(m => _cart.Forward(m));
        }
    }
}
