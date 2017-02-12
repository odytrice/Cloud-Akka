using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using CloudAkka.ActorModel.Models;

namespace CloudAkka.ActorModel.Messages.Events
{
    public class CartStatus
    {
        public string User { get; }
        public Item[] Items { get; }

        public CartStatus(string user, Item[] items)
        {
            User = user;
            Items = items;
        }
    }
}
