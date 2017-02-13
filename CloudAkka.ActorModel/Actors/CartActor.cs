using Akka.Actor;
using CloudAkka.ActorModel.Messages.Commands;
using CloudAkka.ActorModel.Messages.Events;
using CloudAkka.ActorModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudAkka.ActorModel.Actors
{
    public class CartActor : ReceiveActor
    {
        private List<Item> _items = new List<Item>();

        public decimal Total => _items.Sum(i => i.Price * i.Quantity);

        public CartActor()
        {
            Receive<AddProduct>(m => AddProduct(m));
        }

        public void AddProduct(AddProduct message)
        {
            var item = _items.Where(i => i.Name == message.Product.Name).FirstOrDefault();
            if (item != null)
            {
                item.Quantity = item.Quantity + 1;
            }
            else
            {
                item = new Item { Name = message.Product.Name, Quantity = 1, Price = message.Product.Price };
                _items.Add(item);
            }

            Sender.Tell(new ProductAdded(message.User, message.Product));
        }
    }
}
