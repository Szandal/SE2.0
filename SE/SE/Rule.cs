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
        private LinkedList<string> Evidence;//

        public int NumberOfEvidences()
        {
            return Evidence.Count();
        }
    }
}