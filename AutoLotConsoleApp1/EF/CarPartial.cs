using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLotConsoleApp1.EF
{
    partial class Car
    {
        public override string ToString()
        {
            return $"{this.CarNickName ?? "**No Name**"} is a {this.Color}"+ $" Model { this.Model}"+$" with ID { this.CarId}.";
        }
    }
}
