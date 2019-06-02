using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Exceptions
{
    class WrongStrategy :Exception 
    {

        public WrongStrategy (string mess) : base(mess)
        {

        }
    }
}
