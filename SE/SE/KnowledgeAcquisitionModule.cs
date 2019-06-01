using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SE
{
    class KnowledgeAcquisitionModule
    {

        private Rule AddRule(string rule)
        {

            if (!CheckRule(rule))
            {
                throw new System.Exception("zła zasada");
            }
            string [] elsements = rule.Split(new char[] {'=','+'});
            List<string> evidence = elsements.ToList();
            evidence.RemoveAt(evidence.Count-1);
            return new Rule(elsements.Last(), evidence);
        }
        private string AddFact(string fakt)
        {
            if (!CheckFact(fakt))
            {
                throw new System.Exception("zły fakt");
            }
            return fakt;
        }

        private void RemoveRule()
        {

        }
        private void RemoveFact()
        {

        }

        private bool CheckRule(string rule)
        {
            if (rule.IndexOf('=') != rule.LastIndexOf('=') || rule.IndexOf('=') == -1)
            {
                return false;
            }

           
            string []fakts = rule.Split(new char[]{'=', '+' });
            foreach(string fakt in fakts)
            {
                if (!CheckFact(fakt))
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckFact(string fakt)
        {
            Regex rx = new Regex(@"[A-Za-z0-9]");
            MatchCollection match = rx.Matches(fakt);
            if (match.Count != 1)
            {
                return false;
            }
            return true;
        }


    }
}
