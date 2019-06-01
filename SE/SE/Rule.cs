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
        private List<string> Evidence;

        public Rule(string conclusion, List<string> evidence)
        {
            this.conclusion = conclusion;
            this.Evidence = evidence;
        }
        public int NumberOfEvidences()
        {
            return Evidence.Count();
        }
    }
}