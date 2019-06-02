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

        public Rule AddRule(string rule)
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
        public string AddFact(string fact)
        {
            if (CheckFact(fact))
                return fact;
            return null;
        }

        public void RemoveRule()
        {

        }
        public void RemoveFact()
        {

        }

        private bool CheckRule(string rule)
        {
            if (rule.IndexOf('=') == -1 || rule.IndexOf('=') != rule.LastIndexOf('='))
            {
                //zrobić własne wyjątki
                throw new Exception("zła zasada");
            }

           
            string []fakts = rule.Split(new char[]{'=', '+' });
            foreach(string fakt in fakts)
            {
                if (!CheckFact(fakt))
                {
                    //zrobić własne wyjątki
                    throw new Exception("zła zasada");
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
