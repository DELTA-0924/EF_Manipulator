using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLotConsoleApp1.EF
{
     partial class Order
    {
        public override string ToString()
        {
            return $"Order ->{this.OrderId}.";
        }
    }
}
