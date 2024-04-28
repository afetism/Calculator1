using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>\
   

    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private string _labelDown = "0";

        public event PropertyChangedEventHandler? PropertyChanged;

        public string LabelDown
        {
            get { return _labelDown; }
            set
            {
                if (_labelDown != value)
                {
                    _labelDown = value;
                    OnPropertyChanged(nameof(LabelDown));
                }
            }
        }
        private string _labelUp = "0";

        public string LabelUp {
            get => _labelUp;
            set
            {
                if (value != _labelUp)
                {
                    _labelUp = value;
                    OnPropertyChanged(nameof(LabelUp));
                }
            }
                
                
               } 

        public MainWindow()
        {
            InitializeComponent();
            DataContext=this;

        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool isFirstOperation { get; set; } = true;
        public bool IsClickOperator { get; set; } = false;

        private void Button_Equal(object sender, RoutedEventArgs e)
        {

            if (!LabelUp.Contains('='))
            {
                var _operator = LabelUp[^1];
                Calculator.Number1=IsClickOperator ? decimal.Parse(LabelUp.Substring(0, LabelUp.Length-1)) : decimal.Parse(LabelUp);
                Calculator.Number2=decimal.Parse(LabelDown);
                Calculator.Calculate(_operator);
            }
            else
                Calculator.Calculate(Calculator.Operator);


            LabelUp= IsClickOperator ? $"{Calculator.Number1}{Calculator.Operator}{Calculator.Number2}=" : $"{Calculator.Number2}=";
          
            BufferLabel.Visibility= Visibility.Visible;
            LabelDown =Calculator.Result.ToString();
            isFirstOperation=true;
            Calculator.Number1=Calculator.Result;
        }

        private void Button_Click_Number(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (isFirstOperation)
            {
                LabelDown = btn.Content.ToString();
                isFirstOperation = false;
            }
            else if (LabelDown=="0" && btn.Content.ToString()=="0")
                LabelDown="0";
            else
            {
                LabelDown += btn.Content.ToString();
            }
        }

        private void Button_Click_Op(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            switch (btn.Content.ToString())
            {
                case "+":
                    Calculator.Operator='+';
                    break;
                case "-":
                    Calculator.Operator='-';
                    break;
                case "×":
                    Calculator.Operator='×'; 
                    break;
                case "÷":
                    Calculator.Operator='÷';
                    break;

            }

            if (LabelUp=="0")
            {

                LabelUp=$"{LabelDown} {Calculator.Operator}";
               


            }
            else if(!isFirstOperation)
            {
                var _operator = LabelUp[^1];
                Calculator.Number1=decimal.Parse(LabelUp.Substring(0, LabelUp.Length-1));
                Calculator.Number2=decimal.Parse(LabelDown);
                Calculator.Calculate(_operator);
                LabelUp=$"{Calculator.Result} {Calculator.Operator}";
                LabelDown=Calculator.Result.ToString();
                isFirstOperation=true;

            }
            else
            {
                LabelUp=$"{LabelDown} {Calculator.Operator}";
            }
            BufferLabel.Visibility=Visibility.Visible;
            isFirstOperation=true;
            IsClickOperator = true;
        }

        private void OnlyUseNum1(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if(btn.Content.ToString()=="1/x")
            {
                Calculator.Number1=1;
                Calculator.Number2=decimal.Parse(LabelDown);
                Calculator.Operator='÷';
                Calculator.Calculate('÷');
                if (BufferLabel.Visibility !=Visibility.Visible)
                {
                    LabelUp=$"{Calculator.Number1}{Calculator.Operator}{Calculator.Number2}=";
                    LabelDown=Calculator.Result.ToString();
                    BufferLabel.Visibility= Visibility.Visible;
                }
            }
            isFirstOperation=true;
        }

 
        private void Button_Click_Dot(object sender, RoutedEventArgs e)
        {
            if(isFirstOperation)
            {
                LabelDown="0.";
                isFirstOperation=false;
            }
            else if (!LabelDown.Contains('.'))
            {
                LabelDown +=".";
            }
            
        }
    }
}