﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace lab4
{
    public class ServerUser
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public OperationContext operationContext { get; set; }
    }
}
