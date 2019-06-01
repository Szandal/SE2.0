using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE
{
    class KnowledgeBaseModule
    {
        private LinkedList<Rule> ListOfRules;
        private LinkedList<string> ListOfFacts;

        public KnowledgeBaseModule()
        {
            ListOfRules = new LinkedList<Rule>();
            ListOfFacts = new LinkedList<string>();
        }
        public void AddRule(Rule Rule)
        {
            ListOfRules.AddLast(Rule);

        }

    }
}