using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Concurrent;
using CloudAkka.Web.Models;
using CloudAkka.ActorModel.Models;
using CloudAkka.Web.Utils;
using System.Linq;

namespace CloudAkka.Web.Hubs
{
    [HubName("Cart")]
    public class CartHub : Hub
    {
        private readonly static ConcurrentDictionary<string, Cart> _carts = new ConcurrentDictionary<string, Cart>();
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var user = _connections.GetKey(Context.ConnectionId);
            if (user != null) _connections.Remove(user, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public void LoginUser(string userName)
        {
            //Initialize Cart if It doesn't exist
            if (!_carts.ContainsKey(userName))
            {
                _carts[userName] = new Cart();
            }

            //Add Connection to User
            _connections.Add(userName, Context.ConnectionId);

            var cart = _carts[userName];
            OnCartLoaded(userName, cart);
        }


        private void OnCartLoaded(string user, Cart cart)
        {
            //Broadcast to Just this User's Connections
            var clients = _connections.GetConnections(user).Select(c => Clients.Client(c));
            clients.ToList().ForEach(c => c.CartLoaded(cart));
        }

        public void AddProduct(Product product)
        {
            //Get User
            var user = _connections.GetKey(Context.ConnectionId);

            //Get Cart
            if (_carts.ContainsKey(user))
            {
                _carts[user].Add(product);

                OnProductAdded(user, product);
            }
        }

        private void OnProductAdded(string user, Product product)
        {
            //Broadcast to Just this User's Connections
            var clients = _connections.GetConnections(user).Select(c => Clients.Client(c));
            clients.ToList().ForEach(c => c.ProductAdded(product));
        }
    }
}