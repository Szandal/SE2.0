using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE
{
    class InferenceModule
    {
        RulesStrategy RulesStrategy;
        public InferenceModule()
        {
            RulesStrategy = new RulesStrategy();
        }



        public LinkedList<string> ForwardInference(KnowledgeBaseModule KnowledgeBase)
        {
            List<Rule> RulesToUse = new List<Rule>(KnowledgeBase.GetRules());
            List<Rule> NotUsed = new List<Rule>();
            int numberOfNotUsed = 0;
            while(RulesToUse!=NotUsed)
            {
                Rule Rule = RulesStrategy.GetRule(RulesToUse);
                if (RuleCanBeUsed(Rule, KnowledgeBase.GetFacts()) && !KnowledgeBase.GetFacts().Contains(Rule.GetConclusion()))
                {
                    KnowledgeBase.AddFact(Rule.GetConclusion());
                }
                else
                {
                    if (!KnowledgeBase.GetFacts().Contains(Rule.GetConclusion()))
                    {
                        NotUsed.Add(Rule);
                    }
                }
                RulesToUse.Remove(Rule);
                if(RulesToUse.Count() == 0)
                {
                    if (NotUsed.Count() == numberOfNotUsed)
                    {
                        break;
                    }
                    RulesToUse = NotUsed.ToList();
                    //RulesToUse.AddRange(NotUsed);
                    numberOfNotUsed = NotUsed.Count();
                    NotUsed.Clear();
                }
            }
            return KnowledgeBase.GetFacts();
        }


        public bool BackwardsInference(string hypothesis, KnowledgeBaseModule KnowledgeBase)
        {
            List<Rule> LocalRuleList = new List<Rule>(KnowledgeBase.GetRules());  

            while (!KnowledgeBase.GetFacts().Contains(hypothesis))
            {
                if (LocalRuleList.Count() == 0)
                {
                    return false;
                }
                Rule Rule;
                try
                {
                    Rule = RulesStrategy.GetRule(LocalRuleList, hypothesis);
                }
                catch
                {
                    return false;
                }
                
                if (RuleCanBeUsed(Rule, KnowledgeBase.GetFacts()))
                {
                    KnowledgeBase.AddFact(hypothesis);
                }
                else
                {
                    List<bool> ListOfEvidenceCanBeUsed = new List<bool>();
                    foreach(string evidence in Rule.GetEvidence())
                    {
                        ListOfEvidenceCanBeUsed.Add(BackwardsInference(evidence, KnowledgeBase));
                    }
                    if (ListOfEvidenceCanBeUsed.Contains(false))
                    {
                        LocalRuleList.Remove(Rule);
                    }
                    else
                    {
                        KnowledgeBase.AddFact(hypothesis);
                    }
                    
                }
            }
            return true;
        }

        private bool RuleCanBeUsed(Rule Rule, LinkedList<string> Facts)
        {
            foreach(string evidence in Rule.GetEvidence())
            {
                if(!Facts.Contains(evidence))
                {
                    return false;
                }
            }
            return true;
        }
        public void SetActiveStrategy(string NewStrategy)
        {
            RulesStrategy.SetActiveStrategy(NewStrategy);
        }

    }
} 