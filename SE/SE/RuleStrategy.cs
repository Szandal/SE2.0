using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE
{
    class RulesStrategy
    {
        public Rule GetRule(string Strategy, List<Rule> ListOfRules)
        {
            switch (Strategy)
            {
                case "1":
                    return null;
                    break;
                case "2":
                    return null;
                    break;
                default:
                    throw new Exception();
            }
        }

        private Rule Strategia1()
        {
            return null;
        }

        private Rule Strategia2()
        {
            return null;
        }

    }
}