using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoLotConsoleApp1.EF;

namespace AutoLotConsoleApp1
{
    public class Program:Functions
    {
        static  async Task Main(string[] args)
        {

            Program program = new Program();
             
             await program.EnergyLoad();
            
            Console.ReadLine();
        }

    }
 
}

