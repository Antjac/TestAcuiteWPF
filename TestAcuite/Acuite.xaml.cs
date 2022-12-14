using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestAcuite.Class;
using TestAcuite.ViewModels;

namespace TestAcuite
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Acuite : Window
    {
        private AcuiteViewModel _viewModel;
        public Acuite()
        {
            InitializeComponent();
            DataContext = new AcuiteViewModel();
            _viewModel = DataContext as AcuiteViewModel;
            this.KeyDown += new KeyEventHandler(_viewModel.OnKeyPressed);
            WeakReferenceMessenger.Default.Register<NotificationMessage>(this, (r, m) =>
            {
                 if (m.Value == "CloseAcuite")
                 {
                    this.Close();
                 }
            });
        }
    }
}