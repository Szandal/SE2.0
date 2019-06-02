using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Exceptions
{
    class EmptyRuleList : Exception
    {
        public EmptyRuleList(string mess) : base(mess)
        {

        }
    }
}
