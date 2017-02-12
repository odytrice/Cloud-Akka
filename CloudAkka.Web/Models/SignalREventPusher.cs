using CloudAkka.ActorModel.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CloudAkka.ActorModel.Models;
using Microsoft.AspNet.SignalR;
using CloudAkka.Web.Hubs;

namespace CloudAkka.Web.Models
{
    public class SignalREventPusher : IEventPusher
    {
        public static readonly IHubContext _cartHubContext = GlobalHost.ConnectionManager.GetHubContext<CartHub>();

        public void OnCartLoaded(string user, Item[] items)
        {
            //Broadcast to Just this User's Connections
            var clients = CartHub.Connections.GetConnections(user).Select(c => _cartHubContext.Clients.Client(c));
            clients.ToList().ForEach(c => c.CartLoaded(items));
        }

        public void OnProductAdded(string user, Product product)
        {
            //Broadcast to Just this User's Connections
            var clients = CartHub.Connections.GetConnections(user).Select(c => _cartHubContext.Clients.Client(c));
            clients.ToList().ForEach(c => c.ProductAdded(product));
        }
    }
}