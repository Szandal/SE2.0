using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE
{
    class Inference
    {

        public string Rule { get; set; }
        public string Fact { get; set; }

        public Inference(string rule, string facts)
        {

            Rule = rule;
            Fact = facts;
     
        }


    }
}
