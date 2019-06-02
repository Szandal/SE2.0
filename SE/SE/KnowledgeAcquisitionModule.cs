﻿using System;
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
        public string AddFact(string fakt)
        {
            if (CheckFact(fakt))
                return fakt;
            return null;
        }

        public void RemoveRule(Rule Rule , List<Rule> ListOfRules)
        {
            ListOfRules.Remove(Rule);
        }
        public void RemoveFact(string Fact, LinkedList<string> ListOfFacts)
        {
            ListOfFacts.Remove(Fact);
        }

        private bool CheckRule(string rule)
        {
            if (rule.IndexOf('=') == -1 || rule.IndexOf('=') != rule.LastIndexOf('='))
            {
                throw new Exception("zła zasada");
            }

           
            string []fakts = rule.Split(new char[]{'=', '+' });
            foreach(string fakt in fakts)
            {
                if (!CheckFact(fakt))
                {
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
