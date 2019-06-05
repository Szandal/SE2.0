using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Exceptions
{
    class UnsuccessfulInference : Exception
    {
        public UnsuccessfulInference(string mess)  : base(mess)
            {
            }
    }
}
