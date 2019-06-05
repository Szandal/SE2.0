using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SE.Exceptions;

namespace SE
{
    class RulesStrategy
    {


        private string ActiveStrategy = "";
        public Rule GetRule( List<Rule> ListOfRules)
        {
            switch (ActiveStrategy)
            {
                case "FreshnessStrategy":
                    return FreshnessStrategy(ListOfRules);
                case "StrategyOfSpecificity":
                    return StrategyOfSpecificity(ListOfRules);
                default:
                    throw new WrongStrategy("Aktywna strategia :" + ActiveStrategy);
            }
        }

        public Rule GetRule(List<Rule> ListOfRules, string hypothesis)
        {
            switch (ActiveStrategy)
            {
                case "FreshnessStrategy":
                    return FreshnessStrategy(ListOfRules.Where(x => x.GetConclusion() == hypothesis).ToList());
                case "StrategyOfSpecificity":
                    return StrategyOfSpecificity(ListOfRules.Where(x => x.GetConclusion() == hypothesis).ToList());
                default:
                    throw new WrongStrategy("Aktywna strategia :" +ActiveStrategy);
            }
        }


        private Rule FreshnessStrategy(List<Rule> ListOfRules)
        {
            if(ListOfRules.Count()>0)
            return ListOfRules.Last();

            throw new EmptyRuleList("Brak reguł");
        }

        private Rule StrategyOfSpecificity(List<Rule> ListOfRules)
        {
           

            var rule = ListOfRules.OrderByDescending(x => x.GetEvidence().Count()).First();
            //  var biggest1 = ListOfRules.Aggregate((i1, i2) => i1.GetEvidence().Count() > i2.GetEvidence().Count() ? i1 : i2);
            return rule;

            throw new EmptyRuleList("Brak reguł");

        }

        public void SetActiveStrategy(string NewStrategy)
        {
            ActiveStrategy = NewStrategy;
        }
        
    }
}