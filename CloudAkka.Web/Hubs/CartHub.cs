using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using CloudAkka.Web.Models;
using CloudAkka.ActorModel.Models;
using System.Linq;
using CloudAkka.ActorModel.Messages.Commands;

namespace CloudAkka.Web.Hubs
{
    [HubName("Cart")]
    public class CartHub : Hub
    {
        public static ConnectionMapping<string> Connections { get; } = new ConnectionMapping<string>();

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var user = Connections.GetKey(Context.ConnectionId);
            if (user != null) Connections.Remove(user, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        #region Actions

        public void LoginUser(string userName)
        {
            //Add Connection to User
            Connections.Add(userName, Context.ConnectionId);
            AkkaSystem.Actors.Bridge.Tell(new Login(userName), null);
        }

        public void AddProduct(Product product)
        {
            //Get User
            var user = Connections.GetKey(Context.ConnectionId);
            AkkaSystem.Actors.Bridge.Tell(new AddProduct(user, product), null);
        }
        #endregion
    }
}