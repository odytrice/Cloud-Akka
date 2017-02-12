using CloudAkka.ActorModel.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudAkka.Web.Models
{
    public class Cart
    {
        public ConcurrentBag<Item> Items { get; } = new ConcurrentBag<Item>();

        public decimal Total => Items.Sum(i => i.Price * i.Quantity);


        public void Add(Product product)
        {
            var item = Items.Where(i => i.Name == product.Name).FirstOrDefault();
            if (item != null)
            {
                item.Quantity = item.Quantity + 1;
            }
            else
            {
                item = new Item { Name = product.Name, Quantity = 1, Price = product.Price };
                Items.Add(item);
            }
        }
    }
}