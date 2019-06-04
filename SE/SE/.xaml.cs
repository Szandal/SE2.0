﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace SE
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        // Inicjalizacja modułów systemu ekspertowego 
        #region Inicjalizacja 
        KnowledgeAcquisitionModule KnowledgeAcquisitionModule = new KnowledgeAcquisitionModule();
        KnowledgeBaseModule KnowledgeBaseModule = new KnowledgeBaseModule();
        InferenceModule InferenceModule = new InferenceModule();
        ExplanatoryModule ExplanatoryModule = new ExplanatoryModule();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            RuleList.ItemsSource = KnowledgeBaseModule.GetRules();
            InitializeKnowledgeBase();
        }


        private void InitializeKnowledgeBase()
        {
            string facts = "ABCDEFGK";
            foreach(char fact in facts)
            {
                KnowledgeBaseModule.AddFact(fact.ToString());
            }
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("A+B+C=D"));
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("A+B=H"));
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("H+C=Z"));
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("G+S=R"));
        }

        private void OnAddRule(object sender, RoutedEventArgs e)
        {
            InputBox ip = new InputBox("Huj");
            Console.Write(ip.GetText())
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("B+C=A"));
        //        KnowledgeBaseModule.AddFact("B");
        //        KnowledgeBaseModule.AddFact("C");
        //        InferenceModule.SetActiveStrategy("FreshnessStrategy");

        //        //RuleBox.Text = InferenceModule.BackwardsInference("A", KnowledgeBaseModule) ? "tak" : "Nie";
        //        RuleBox.Text = InferenceModule.ForwardInference(KnowledgeBaseModule).Last();
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //}
    }
}
