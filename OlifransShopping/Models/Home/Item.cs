using OlifransShopping.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OlifransShopping.Models.Home
{
    public class Item
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}