using Akka.Actor;
using CloudAkka.ActorModel.Messages.Commands;
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
            Receive<AddProduct>(m => AddProduct(m.Product));
        }

        public void AddProduct(Product product)
        {
            var item = _items.Where(i => i.Name == product.Name).FirstOrDefault();
            if (item != null)
            {
                item.Quantity = item.Quantity + 1;
            }
            else
            {
                item = new Item { Name = product.Name, Quantity = 1, Price = product.Price };
                _items.Add(item);
            }
        }
    }
}
