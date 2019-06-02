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
            return KnowledgeBase.GetFacts();
        }


        public bool BackwardsInference(string hypothesis, KnowledgeBaseModule KnowledgeBase)
        {
            List<Rule> LocalRuleList = new List<Rule>();  //niewidzieć czemu usówa liste ze wszystkich rekurencji
            LocalRuleList = KnowledgeBase.GetRules();
            while (!KnowledgeBase.GetFacts().Contains(hypothesis))
            {
                if (LocalRuleList.Count() == 0)
                {
                    return false;
                }
                Rule Rule = RulesStrategy.GetRule("FreshnessStrategy", LocalRuleList, hypothesis);
                if (RuleCanBeUsed(Rule, KnowledgeBase))
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

        private bool RuleCanBeUsed(Rule Rule, KnowledgeBaseModule knowledgeBase)
        {
            foreach(string evidence in Rule.GetEvidence())
            {
                if(!knowledgeBase.GetFacts().Contains(evidence))
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckRuleBackwards(Rule Rule, string hypothesis, KnowledgeBaseModule KnowledgeBase)
        {
            int numberOfCheckedEvidence = 0;
            if (!Rule.GetConclusion().Equals(hypothesis))
            {
                return false;    
            }
            foreach (string fact in Rule.GetEvidence())
            {
                if (KnowledgeBase.GetFacts().Contains(fact))
                {
                    numberOfCheckedEvidence++;
                }
                else
                {
                    if (BackwardsInference(fact, KnowledgeBase))
                    {
                        numberOfCheckedEvidence++;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            if (numberOfCheckedEvidence == Rule.NumberOfEvidences())
            {
                return true;
            }
            return false;
        }
    }
} 