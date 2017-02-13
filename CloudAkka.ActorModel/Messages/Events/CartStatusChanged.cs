using CloudAkka.ActorModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudAkka.ActorModel.Messages.Events
{
    public class CartStatusChanged
    {
        public string User { get; }
        public Item[] Items { get; }

        public CartStatusChanged(string user, Item[] items)
        {
            User = user;
            Items = items;
        }
    }
}
