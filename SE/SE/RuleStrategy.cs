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
                case "FreshnessStrategy":
                    return FreshnessStrategy(ListOfRules);
                case "StrategyOfSpecificity":
                    return StrategyOfSpecificity(ListOfRules);
                default:
                    throw new Exception();
            }
        }

        public Rule GetRule(string Strategy, List<Rule> ListOfRules, string hypothesis)
        {
            switch (Strategy)
            {
                case "FreshnessStrategy":
                    return FreshnessStrategy(ListOfRules.Where(x => x.GetConclusion() == hypothesis).ToList());
                case "StrategyOfSpecificity":
                    return StrategyOfSpecificity(ListOfRules.Where(x => x.GetConclusion() == hypothesis).ToList());
                default:
                    throw new Exception();
            }
        }


        private Rule FreshnessStrategy(List<Rule> ListOfRules)
        {
            return ListOfRules.Last();
        }

        private Rule StrategyOfSpecificity(List<Rule> ListOfRules)
        {
            bool Top;
            foreach (var rule in ListOfRules)
            {
                Top = true;

                foreach (var rule2 in ListOfRules)
                {
                    if (rule.NumberOfEvidences() < rule2.NumberOfEvidences())
                    {
                        Top = false;
                        break;

                    }
                }

                if(Top)
                return rule;
            }

            throw new Exception();

        }

    }
}