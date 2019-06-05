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

        public string GetConclusion()
        {
            return Conclusion;
        }

        public List<string> GetEvidence()
        {
            return Evidence;
        }

        public override string ToString()
        {
            string temp = "";
            foreach (var item in Evidence)
            {
                temp += item + "+";

            }
            temp += "=" + Conclusion;
            return temp;
        }

    }
}