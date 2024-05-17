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
namespace wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
      private void testButton_Click(object sender, RoutedEventArgs e)
        {
            var testWindow = new TestWindow();
            testWindow.Show();
            Close();
        }

        private void viewButton_Click(object sender, RoutedEventArgs e)
        {
            var viewWindow = new ViewWindow();
            viewWindow.Show(); 
            Close();
        }
}
public class MotherBoard
{
    public string Mulfunction;
    public Dictionary<string,string> Data = new Dictionary<string, string>()
    {
    { "CMOS", "3 В"},
    { "USB1", "5 В"},
    { "USB2", "5 В"},
    { "PLTRST","3.3 В"},
    { "SYS_PWROK","3.3 В"},
    { "RT7296F_PG","1 В"}, 
    { "ClockSin","1"},
    { "BIOSSin","1"},
    { "FiveV","4.2 КОм"},
    { "VCCinAUX", "1,8 В"},
    { "VCore", "1,2 В"},
    { "AUXEnable", "3.3 В"},
    { "PWMAUX", "1"},
    { "Molex", "12 В 5 В"},
    { "SMD", "1"},
    };
    public double CmosVoltage = 3;
    public double USBVoltage = 5;
    public double PLTRSTVoltage = 0;
    public double SYS_PWROKVoltage = 3.3;
    public double RT7296F_PGVoltage = 3.3;//no
    public bool ClockSin = true;
    public bool BIOSSin  = true;

}
