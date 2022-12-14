using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using TestAcuite.Class;
using TestAcuite.Helpers;
namespace TestAcuite.ViewModels
{
    internal class CalibrationViewModel : INotifyPropertyChanged
    {
        public ICommand CalibrationValidationCommand { get; }
        public ICommand GoToAcuiteCommand { get;}

        private CalibrationParams _params = new CalibrationParams();
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public event PropertyChangedEventHandler PropertyChanged;

        #region Construcors
        public CalibrationViewModel()
        {
            CalibrationParams p = ConfigHelper.GetCalibration();
            if (p is null)
            {
                _params.Accuity = 1.0M;
                _params.TextSize = 7;
                _params.Distance = 400;
                _params.FontSize = 500;
                ConfigHelper.SaveCalibration(_params);
            }
            else
            {
                _params.Accuity = p.Accuity;
                _params.TextSize = p.TextSize;
                _params.Distance = p.Distance;
                _params.FontSize = p.FontSize;
            }
            CalibrationValidationCommand = new RelayCommand(SaveCalibration);
            GoToAcuiteCommand = new RelayCommand(GoToAcuite);

        }
        #endregion

        #region Properties
        public int FontSize
        {
            get { return _params.FontSize; }
            set
            {
                _params.FontSize = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }

        public decimal Accuity
        {
            get { return _params.Accuity; }
            set
            {
                _params.Accuity = value;
                OnPropertyChanged(nameof(Accuity));
            }
        }

        public decimal TextSize
        {
            get { return _params.TextSize; }
            set
            {
                _params.TextSize = value;
                OnPropertyChanged(nameof(TextSize));
            }
        }

        public int Distance
        {
            get { return _params.Distance; }
            set
            {
                _params.Distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }
        #endregion

        #region commands
        private void SaveCalibration()
        {
            if (ConfigHelper.SaveCalibration(_params))
            {
                ShowSaveCalibrationToast();
            }
        }
    
        private void GoToAcuite()
        {
            Acuite window = new Acuite();
            window.Show();
            notifyWindowToClose();
        }
        #endregion

        #region voids
        private async void ShowSaveCalibrationToast()
        {
            
            MessageBox.Show("Calibration sauvegardée");
        }

        public void notifyWindowToClose()
        {
            WeakReferenceMessenger.Default.Send<NotificationMessage>(
                new NotificationMessage("CloseCalibration")
            );
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}
