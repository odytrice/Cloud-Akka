using CloudAkka.ActorModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudAkka.ActorModel.Messages.Events
{
    public class ProductAdded
    {
        public string User { get; }
        public Product Product { get; }
    }
}
