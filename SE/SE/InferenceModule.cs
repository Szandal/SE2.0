﻿using System;
using System.Collections.Generic;
using System.Linq;
using SE.Exceptions;
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



        public List<Inference> ForwardInference(KnowledgeBaseModule KnowledgeBase, bool HideNotUsed)
        {
            List<Inference> inferences = new List<Inference>();
            List<Rule> RulesToUse = new List<Rule>(KnowledgeBase.GetRules());
            List<Rule> NotUsed = new List<Rule>();
            inferences.Add(new Inference("Posiadane fakty:", KnowledgeBase.ToString()));
            int numberOfNotUsed = 0;
            while(RulesToUse!=NotUsed)
            {
                Rule Rule = RulesStrategy.GetRule(RulesToUse);
               
                if (RuleCanBeUsed(Rule, KnowledgeBase.GetFacts()) && !KnowledgeBase.GetFacts().Contains(Rule.GetConclusion()))
                {
                    KnowledgeBase.AddFact(Rule.GetConclusion());
                    inferences.Add(new Inference( Rule.GetRule(), KnowledgeBase.ToString()));
                    
                }
                else
                {
                    if(!HideNotUsed)
                        inferences.Add(new Inference(Rule.GetRule(), "Not Used"));
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
                    NotUsed.Reverse();
                    RulesToUse = NotUsed.ToList();
                    //RulesToUse.AddRange(NotUsed);
                    numberOfNotUsed = NotUsed.Count();
                    NotUsed.Clear();
                }
            }
            inferences.Add(new Inference("Posiadane fakty:", KnowledgeBase.ToString()));
            return inferences;
        }


        public bool BackwardsInference(string hypothesis, KnowledgeBaseModule KnowledgeBase, List<Inference> inferences)
        {
            List<Rule> LocalRuleList = new List<Rule>(KnowledgeBase.GetRules());  

            while (!KnowledgeBase.GetFacts().Contains(hypothesis))
            {
                
                if (LocalRuleList.Count() == 0)
                {
                    throw new UnsuccessfulInference("Nieudane wnioskowanie");
                }
                Rule Rule;
                try
                {
                    Rule = RulesStrategy.GetRule(LocalRuleList, hypothesis);
                }
                catch
                {
                    throw new WrongRule("Nie znaleziono reguły dla hipotezy :" +hypothesis);
                }
                
                if (RuleCanBeUsed(Rule, KnowledgeBase.GetFacts()))
                {
                    KnowledgeBase.AddFact(hypothesis);
                    inferences.Add(new Inference(Rule.GetRule(), KnowledgeBase.ToString()));
                }
                else
                {
                    List<bool> ListOfEvidenceCanBeUsed = new List<bool>();
                    foreach(string evidence in Rule.GetEvidence())
                    {
                        inferences.Add(new Inference("Rekurencyjne wywołanie dla hipotezy: " + evidence + "\nw regule: " + Rule.GetRule(), KnowledgeBase.ToString()));
                        ListOfEvidenceCanBeUsed.Add(BackwardsInference(evidence, KnowledgeBase,inferences));
                    }
                    if (ListOfEvidenceCanBeUsed.Contains(false))
                    {
                        inferences.Add(new Inference(Rule.GetRule(), "-!-"));
                        LocalRuleList.Remove(Rule);
                    }
                    else
                    {
                        inferences.Add(new Inference(Rule.GetRule(), KnowledgeBase.ToString()));
                        KnowledgeBase.AddFact(hypothesis);
                    }
                    
                }
            }

            inferences.Add(new Inference("Hipoteza "+ hypothesis +" znajduje się w bazie faktów",KnowledgeBase.ToString()));
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