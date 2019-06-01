using System;
using System.Collections.Generic;
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

namespace SE
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Inicjalizacja modułów systemu ekspertowego 
        #region Inicjalizacja 
        KnowledgeAcquisitionModule KnowledgeAcquisitionModule = new KnowledgeAcquisitionModule();
        KnowledgeBaseModule KnowledgeBase = new KnowledgeBaseModule();
        InferenceModule InferenceModule = new InferenceModule();
        ExplanatoryModule ExplanatoryModule = new ExplanatoryModule();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }



    }
}
