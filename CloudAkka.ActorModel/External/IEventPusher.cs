using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudAkka.ActorModel.Models;

namespace CloudAkka.ActorModel.External
{
    public interface IEventPusher
    {
        void OnCartLoaded(string user, Item[] items);
        void OnProductAdded(string user, Product product);
    }
}
