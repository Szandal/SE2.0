using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE
{
    class Rule
    {
        private string conclusion;
        private List<string> evidence;

        public Rule(string conclusion, List<string> evidence)
        {
            this.conclusion = conclusion;
            this.evidence = evidence;
        }

    }
}