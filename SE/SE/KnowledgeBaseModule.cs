using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE
{
    class KnowledgeBaseModule 
    {
        private List<Rule> ListOfRules;
        private LinkedList<string> ListOfFacts;
        

        public KnowledgeBaseModule()
        {
            ListOfRules = new List<Rule>();
            ListOfFacts = new LinkedList<string>();
        }
        public void AddRule(Rule Rule)
        {
            ListOfRules.Add(Rule);
        }
        public void AddFact(string fact)
        {
            ListOfFacts.AddLast(fact);
        }
        public LinkedList<string> GetFacts()
        {
            return ListOfFacts;
        }
        public List<Rule> GetRules()
        {
            return ListOfRules;
        }

        public override string ToString()
        {
            var temp = string.Concat(ListOfFacts.ToArray());
            return temp;
        }
    }
}