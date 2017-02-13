using Akka.Actor;
using CloudAkka.ActorModel.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudAkka.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var actorSystem = ActorSystem.Create("ShoppingSystem");
            var shoppingActor = actorSystem.ActorOf<ShoppingActor>("ShoppingActor");
            actorSystem.WhenTerminated.Wait();
        }
    }
}
