using CloudAkka.ActorModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudAkka.ActorModel.Messages.Commands
{
    public class AddProduct
    {
        public string User { get; }
        public Product Product { get; }

        public AddProduct(string user, Product product)
        {
            User = user;
            Product = product;
        }
    }
}
