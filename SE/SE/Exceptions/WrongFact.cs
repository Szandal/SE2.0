﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Exceptions
{
    class WrongFact : Exception
    {
       public WrongFact(string mess) : base(mess)
        {
        

        }
    }
}
