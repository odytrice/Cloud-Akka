using Akka.Actor;
using CloudAkka.ActorModel.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAkka.Web.Models
{
    public static class AkkaSystem
    {
        private static ActorSystem actorSystem;

        public static void Create()
        {
            actorSystem = ActorSystem.Create("CartSystem");
            Actors.CartController = actorSystem.ActorOf<ShoppingActor>();


            var signalRProps = Props.Create<SignalRActor>(null,Actors.CartController);
            Actors.Bridge = actorSystem.ActorOf(signalRProps, "SignalrActor");
        }

        public static void Shutdown()
        {
            actorSystem.Terminate().Wait();
        }

        public static class Actors
        {
            public static IActorRef CartController { get; set; }
            public static IActorRef Bridge { get; set; }
        }
    }
}