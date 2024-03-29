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
using MahApps.Metro.Controls.Dialogs;

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
            //await this.ShowMessageAsync("This is the title", "Some message");
        }

    }
}
