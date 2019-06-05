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
        private readonly string FullRule;
        public Rule(string Conclusion, List<string> Evidence, string FullRule)
        {
            this.FullRule = FullRule;
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
        public string GetRule()
        {
            return FullRule;
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