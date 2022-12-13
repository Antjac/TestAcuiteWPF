using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TestAcuite.Class;
using TestAcuite.Helpers;

namespace TestAcuite.ViewModels
{
    class AcuiteViewModel : INotifyPropertyChanged
    {
        private double _fontSize;
        private CalibrationParams _params;
        private String _textToShow;
        private String _textColor;
        private String _backgroundColor;
        private decimal _currentLogMar;
        private List<String> _lstToShow = new();
        private readonly List<String> _lstSloan = new() { "N C K Z O ", "R H S D K ", "D O V H R ", "O N H R C ", "D K S N V ", "Z S O K N ", "C K D N R ", "S R Z K D ", "H Z O V C ", "N V D O K ", "V H C N O ", "S V H C Z ", "O Z D V K " };
        private readonly List<String> _lstRaskin = new() { "A D B C D ", "C B A D C ", "D C B A B ", "B A D C A " };
        private readonly List<String> _lstLandolt = new() { "E K G H I ", "L F E K J ", "K E H F I ", "F K H I G ", "E J F L H ","H K I H L ","I J F E G ","F I H E K " };
        private const double LOGMAR_COEFF = 1.2589d;
        private int _nbLetters;
        private int _scaleX;
        private string _fontToUse;
        private readonly ConvertUnit[] _convert = new ConvertUnit[]
        {
            new ConvertUnit(-0.3M,2,"20/10"),
            new ConvertUnit(-0.2M,1.6,"16/10"),
            new ConvertUnit(-0.1M,1.25,"12,5/10"),
            new ConvertUnit(0,1,"10/10"),
            new ConvertUnit(0.1M,0.8,"8/10"),
            new ConvertUnit(0.2M,0.63,"6,3/10"),
            new ConvertUnit(0.3M,0.5,"5/10"),
            new ConvertUnit(0.4M,0.4,"4/10"),
            new ConvertUnit(0.5M,0.32,"3.2/10"),
            new ConvertUnit(0.6M,0.25,"2.5/10"),
            new ConvertUnit(0.7M,0.2,"2/10"),
            new ConvertUnit(0.8M,0.16,"1.6/10"),
            new ConvertUnit(0.9M,0.125,"1.25/10"),
            new ConvertUnit(1M,0.1,"1/10"),
            new ConvertUnit(1.1M,0.08,"1/12"),
            new ConvertUnit(1.2M,0.063,"1/16"),
            new ConvertUnit(1.3M,0.05,"1/20"),
            new ConvertUnit(1.4M,0.04,"1/25"),
            new ConvertUnit(1.5M,0.033,"1/30"),
            new ConvertUnit(1.6M,0.025,"1/40"),
            new ConvertUnit(1.7M,0.020,"1/50")
            /*new ConvertUnit(1.8M,0.016,"1/60"),
            new ConvertUnit(1.9M,0.0125,"1/80"),
            new ConvertUnit(2M,0.010,"1/100"),
            new ConvertUnit(2.1M,0.008,"1/120")*/
        };
        private bool _isAcuiteVisible;
        private bool _isHelpVisible;

        #region Constructors
        public AcuiteViewModel()
        {
            _isHelpVisible = true;
            _isAcuiteVisible = false;
            _textColor = "White";
            _backgroundColor = "Black";
            _isAcuiteVisible = true;
            _scaleX = 1;
            _nbLetters = 3;
            _fontToUse = "Resources/Fonts/#Sloan";
            _lstToShow = _lstSloan;
            
            Listen();
            ShowCombinaison();
        }
        #endregion

        #region Properties
        public String TextToShow
        {
            get { return _textToShow; }
            set { _textToShow = value; OnPropertyChanged(nameof(TextToShow)); }
        }

        public String FontToUse
        {
            get { return _fontToUse; }
            set { _fontToUse = value; OnPropertyChanged(nameof(FontToUse)); }
        }

        public double FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; OnPropertyChanged(nameof(FontSize)); OnPropertyChanged(nameof(Padding)); }
        }

        public String TextColor
        {
            get { return _textColor; }
            set { _textColor = value; OnPropertyChanged(nameof(TextColor)); }

        }

        public String Padding
        {
            get { 
                return $"0,{((int)(FontSize*0.25))},0,0"; 
            }
        }

        public ConvertUnit CurentAcuite
        {
            get
            {
                ConvertUnit currentVal = _convert.FirstOrDefault(x => x.Logmar.Equals(_currentLogMar));
                return currentVal;
            }
        }

        public Boolean IsAcuiteVisible
        {
            get { return _isAcuiteVisible; }
            set { _isAcuiteVisible = value; OnPropertyChanged(nameof(IsAcuiteVisible)); }
        }

        public int ScaleXValue
        {
            get { return _scaleX; }
            set { _scaleX = value; OnPropertyChanged(nameof(ScaleXValue)); }
        }

        public Boolean IsHelpVisible
        {
            get { return _isHelpVisible; }
            set { _isHelpVisible = value; OnPropertyChanged(nameof(IsHelpVisible)); }
        }

        public String BackGroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; OnPropertyChanged(nameof(BackGroundColor)); }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Events

        #region Voids
        public void Listen()
        {
            _params = ConfigHelper.GetCalibration();
            _currentLogMar = _params.Accuity;
            FontSize = _params.FontSize;

            OnPropertyChanged(nameof(CurentAcuite));
        }

        public void OnKeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad1:
                    _nbLetters = 1;
                    ShowCombinaison();
                    break;

                case Key.NumPad2:
                    _nbLetters = 2;
                    ShowCombinaison();
                    break;
                case Key.NumPad3:
                    _nbLetters = 3;
                    ShowCombinaison();
                    break;
                case Key.NumPad4:
                    _nbLetters = 4;
                    ShowCombinaison();
                    break;
                case Key.NumPad5:
                    _nbLetters = 5;
                    ShowCombinaison();
                    break;
                case Key.Enter:
                    IsAcuiteVisible = !IsAcuiteVisible;
                    break;
                case Key.Space:
                    if (TextColor == "Black")
                    {
                        TextColor = "White";
                        BackGroundColor = "Black";
                    }
                    else
                    {
                        TextColor = "Black";
                        BackGroundColor = "White";
                    }
                    break;
                case Key.H:
                    IsHelpVisible = !IsHelpVisible;
                    break;
                case Key.M:
                    ScaleXValue = ScaleXValue *= -1;
                    break;
                case Key.F1:
                    FontToUse = "Resources/Fonts/#Sloan";
                    _lstToShow = _lstSloan;
                    ShowCombinaison();
                    break;
                case Key.F2:
                    FontToUse = "Resources/Fonts/#Raskin_Landolt";
                    _lstToShow = _lstRaskin;
                    ShowCombinaison();
                    break;
                case Key.F3:
                    FontToUse = "Resources/Fonts/#Raskin_Landolt";
                    _lstToShow = _lstLandolt;
                    ShowCombinaison();
                    break;
                case Key.Add:
                    DecreaseTextSize();
                    break;
                case Key.Subtract:
                    IncreaseTextSize();
                    break;

                case Key.Escape:
                    Calibration calibration = new Calibration();
                    calibration.Show();
                    notifyWindowToClose();
                    break;
            }
        }

        private void ShowCombinaison()
        {
            var random = new Random();
            int index = random.Next(_lstToShow.Count);
            TextToShow = _lstToShow[index].Substring(random.Next(0, _lstToShow[index].Count() - _nbLetters *2), _nbLetters*2).Trim();
        }
        private void IncreaseTextSize()
        {
            ConvertUnit nextVal = _convert.FirstOrDefault(x => x.Logmar.Equals(_currentLogMar + 0.1M));
            if (nextVal == null)
            {
                return;
            }

            FontSize *= LOGMAR_COEFF;
            _currentLogMar = nextVal.Logmar;
            OnPropertyChanged(nameof(CurentAcuite));
            ShowCombinaison();
        }

        private void DecreaseTextSize()
        {
            ConvertUnit nextVal = _convert.FirstOrDefault(x => x.Logmar.Equals(_currentLogMar - 0.1M));
            if (nextVal == null)
            {
                return;
            }

            FontSize /= LOGMAR_COEFF;
            _currentLogMar = nextVal.Logmar;
            OnPropertyChanged(nameof(CurentAcuite));
            ShowCombinaison();
        }

        public void notifyWindowToClose()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseWindowsBoundToMe")
            );
        }
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        #endregion
    }
}
