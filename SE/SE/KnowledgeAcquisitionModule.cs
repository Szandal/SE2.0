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
            if (CheckRule(rule))
            {
                string [] elsements = rule.Split(new char[] {'=','+'});
                List<string> Evidence = elsements.ToList();
                Evidence.RemoveAt(Evidence.Count-1);
                return new Rule(elsements.Last(), Evidence);
            }
            return null;
        }
        private string AddFact(string fakt)
        {
            if (CheckFact(fakt))
                return fakt;
            return null;
        }

        private void RemoveRule()
        {

        }
        private void RemoveFact()
        {

        }

        private bool CheckRule(string rule)
        {
            if (rule.IndexOf('=') == -1 || rule.IndexOf('=') != rule.LastIndexOf('='))
            {
                throw new System.Exception("zła zasada");
            }
            string []fakts = rule.Split(new char[]{'=', '+' });
            foreach(string fakt in fakts)
            {
                if (!CheckFact(fakt))
                {
                    throw new System.Exception("zła zasada");
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
                throw new System.Exception("zły fakt");
            }
            return true;
        }


    }
}
