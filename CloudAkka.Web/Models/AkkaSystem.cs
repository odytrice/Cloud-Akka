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
            actorSystem = ActorSystem.Create("ShoppingSystem");

            //Get Shopping Actor Remotely
            var actorAddress = "akka.tcp://ShoppingSystem@127.0.0.1:8091/user/ShoppingActor";
            Actors.ShoppingActor = actorSystem.ActorSelection(actorAddress)
                                              .ResolveOne(TimeSpan.FromSeconds(3))
                                              .Result;

            var signalRProps = Props.Create<SignalRActor>(new SignalREventPusher(), Actors.ShoppingActor);
            Actors.Bridge = actorSystem.ActorOf(signalRProps, "SignalrActor");
        }

        public static void Shutdown()
        {
            actorSystem.Terminate().Wait();
        }

        public static class Actors
        {
            public static IActorRef ShoppingActor { get; set; }
            public static IActorRef Bridge { get; set; }
        }
    }
}