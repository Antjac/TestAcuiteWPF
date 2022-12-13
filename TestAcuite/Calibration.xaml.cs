using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using TestAcuite.ViewModels;

namespace TestAcuite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Calibration : Window
    {
        public Calibration()
        {
            InitializeComponent();
            DataContext = new CalibrationViewModel();
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
