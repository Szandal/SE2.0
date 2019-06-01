using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE
{
    class Rule
    {
        private string Conclusion;
        private List<string> Evidence;

        public Rule(string Conclusion, List<string> Evidence)
        {
            this.Conclusion = Conclusion;
            this.Evidence = Evidence;
        }

        public int NumberOfEvidences()
        {
            return Evidence.Count();
        }
    }
}