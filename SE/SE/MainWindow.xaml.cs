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
        List<Inference> Inference = new List<Inference>();
        #endregion

        private readonly IDialogCoordinator _dialogCoordinator;

        public MainWindow(IDialogCoordinator dialogCoordinator)
        {
            InitializeComponent();
            ResetUI();
            InitializeKnowledgeBase();
            //PlaySound();
            ShowHelloMessage();
            _dialogCoordinator = dialogCoordinator;
        }
        public MainWindow()
        {
      
            InitializeComponent();
            ResetUI();
            InitializeKnowledgeBase();
            PlaySound();
            ShowHelloMessage();
        }

        private void ResetUI()
        {
            RuleList.ItemsSource = null;
            FactList.ItemsSource = null;
            InferenceSteps.ItemsSource = null;
            RuleList.ItemsSource = KnowledgeBaseModule.GetRules();
            FactList.ItemsSource = KnowledgeBaseModule.GetFacts();
            InferenceSteps.ItemsSource = Inference;
        }
        private void ShowHelloMessage()
        {
            if(MessageBox.Show("Witaj w piekle, Przypomne ci teraz koszmar SE!!! \nChcesz podjąć wyzwanie?", "Koszmar", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                MessageBox.Show("Już się poddałeś?","Buhahaha");
                this.Close();
            }
            else
            {
                MessageBox.Show("So, Let's play a game!","Wow");
            }
        }

        private void PlaySound()
        {
            System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer();
            soundPlayer.SoundLocation = @"Music\1.wav";
            soundPlayer.Load();
            soundPlayer.Play();
        }

        private void InitializeKnowledgeBase()
        {
            string facts = "ABCEFGK";
            foreach(char fact in facts)
            {
                KnowledgeBaseModule.AddFact(fact.ToString());
            }
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("A+B+C=D"));
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("A+D=H"));
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("H+C=Z"));
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("G+S=R"));
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("AA+SA=RA"));
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("A3+B3+C3=D3"));
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("A3+D3=H3"));
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("H3+C3=Z3"));
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("G3+S3=R3"));
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule("AA3+SA3=RA3"));

        }

        private async void OnAddRuleAsync(object sender, RoutedEventArgs e)
        {
            string newRule = await this.ShowInputAsync("Dodaj Regułę","Wpisz regułę (np. \"A+B=C\")",null);
            try
            {
                if(newRule == null||newRule == ""||!KnowledgeAcquisitionModule.CheckRule(newRule))
                {
                    await this.ShowMessageAsync("?!?!?!", "A podałeś jakiś fakt??");
                    return;
                }
            }catch(Exception ex)
            {
                await this.ShowMessageAsync("Error", ex.Message);
            }
            
            KnowledgeBaseModule.AddRule(KnowledgeAcquisitionModule.AddRule(newRule));
            ResetUI();
        }
        private async void OnAddFactAsync(object sender, RoutedEventArgs e)
        {
            string newFact = await this.ShowInputAsync("Dodaj Fakt", "Wpisz Fakt (np. \"A\",\"Kaszel\" itp.)", null);
            try
            {
                if (newFact == null || newFact == ""||!KnowledgeAcquisitionModule.CheckFact(newFact))
                {
                    await this.ShowMessageAsync("?!?!?!", "A podałeś jakiś fakt??");
                    return;
                }
            }
            catch(Exception ex)
            {
                await this.ShowMessageAsync("Error", ex.Message);
            }
            
            KnowledgeBaseModule.AddFact(newFact);
            ResetUI();
        }
        public ProgressDialogController dialog;
        private async void Forward_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Inference.Clear();
                bool Hide = HideNotUsed.IsChecked.Value;
                Inference = InferenceModule.ForwardInference(KnowledgeBaseModule,Hide);
                ResetUI();
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error", ex.Message);

            }
    
          
          

            dialog = await this.ShowProgressAsync("","", false, null);
            dialog.SetIndeterminate();

            var item = new Window1();
            item.Background = null;

             await this.ShowMetroDialogAsync(item, null);
  
     

            await Task.Run(() =>
            {
                System.Threading.Thread.Sleep(1000);
               
            });
            await dialog.CloseAsync();
            await this.HideMetroDialogAsync(item);
          


        }



    private async void Backward_Click(object sender, RoutedEventArgs e)
        {
            Inference.Clear();
            string hypotes = await this.ShowInputAsync("Podaj hipotezę", "Wpisz Fakt który chcesz udowodnić (np. \"A\",\"Kaszel\" itp.)", null);
            try
            {
                if (hypotes == null || hypotes == ""||!KnowledgeAcquisitionModule.CheckFact(hypotes))
                {
                    await this.ShowMessageAsync("?!?!?!", "A podałeś jakąś hipotezę??");
                    return;
                }
                Inference.Clear();
                InferenceModule.BackwardsInference(hypotes, KnowledgeBaseModule, Inference);
                ResetUI();
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error", ex.Message);
            }
 

        }

        private void InferenceStrategyRadioButton(object sender, RoutedEventArgs e)
        {
            InferenceModule.SetActiveStrategy((sender as RadioButton).Name);
        }

        private async void OnDelRuleAsync(object sender, RoutedEventArgs e)
        {
            int index = RuleList.SelectedIndex;
            if (index != -1)
            {
                KnowledgeBaseModule.DelRule(index);
                RuleList.ItemsSource = null;
                RuleList.ItemsSource = KnowledgeBaseModule.GetRules();
            }
            else
            {
                await this.ShowMessageAsync("Error", "Drogi użytkowniku co ci mam niby usunąć?!");
            }
        }

        private async void OnDelFactAsync(object sender, RoutedEventArgs e)
        {
            string fact;
            try
            {
                fact = FactList.SelectedValue.ToString();
            }
            catch
            {
                await this.ShowMessageAsync("Error", "Drogi użytkowniku co ci mam niby usunąć?!");
                return;
            }
            KnowledgeBaseModule.DelFact(fact);
            FactList.ItemsSource = null;
            FactList.ItemsSource = KnowledgeBaseModule.GetFacts();
        }
        
        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            Payment payment = new Payment();
            payment.payAsync();
           
        }
        
    }
}
