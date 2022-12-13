using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            Messenger.Default.Register<NotificationMessage>(this, (nm) =>
            {
                if (nm.Notification == "CloseWindowsBoundToMe")
                {
                    if (nm.Sender == this.DataContext)
                        this.Close();
                }
            });
        }
    }
}