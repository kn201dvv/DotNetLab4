﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(lab4.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Server started...");
                Console.ReadLine();
            }
        }
    }
}