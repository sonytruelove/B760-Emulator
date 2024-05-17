using System.Windows;
using System.Windows.Media;
namespace wpf;

    public partial class answerResult: Window
    {
        public answerResult(bool isCorrect)
        {
            InitializeComponent();

            if (isCorrect)
            {
                resultText.Text = "Правильный ответ";
                resultText.Foreground = Brushes.Green;
            }
            else
            {
                resultText.Text = "Неправильный ответ";
                resultText.Foreground = Brushes.Red;
            }
        }
    }
