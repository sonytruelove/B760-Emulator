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
using System.Diagnostics;
namespace wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class TestWindow : Window
{
    MotherBoard board;
    MotherBoard[] configurations = {
        new MotherBoard {Mulfunction = "Батарейка CMOS села"},
        new MotherBoard {Mulfunction = "Неисправен южный мост"},
        new MotherBoard {Mulfunction = "Выход из строя часового кварцевового резонатора"},
        new MotherBoard {Mulfunction = "Выход из строя BIOS"},
        new MotherBoard {Mulfunction = "Основной ШИМ контроллер процессора неисправен"},
        new MotherBoard {Mulfunction = "Контроллер RT7296F не исправен"},
        new MotherBoard {Mulfunction = "ШИМ-контроллер фазы питания AUX не исправен"},
        new MotherBoard {Mulfunction = "Сбитые SMD"},
        new MotherBoard {Mulfunction = "Поврежденный сокет"},
        new MotherBoard {Mulfunction = "Molex порт не исправен"},
        new MotherBoard {Mulfunction = "Вентилятор не работает"},
    };
    private bool isMagnifierOn = false;
    private bool isMultimetrOn = false;
    private bool isOsciOn = false;
    private bool isFanOn = false;
    MagnifierAreaData[] Areas = new MagnifierAreaData[5];
    private HashSet<int> usedIndexes = new HashSet<int>();
    public TestWindow()
    {
        InitializeComponent();
      
        Random random = new Random();
        board = configurations[random.Next(0, configurations.Length)];
        switch (board.Mulfunction)
        {
            case "Батарейка CMOS села":
                board.Data["CMOS"] = "1 В";
                break;
            case "Неисправен южный мост":
            int choose = random.Next(3);
            if(choose == 0){
                board.Data["USB1"] = "3 В";
                board.Data["USB2"] = "5 В";}
            if(choose == 1)
                board.Data["SYS_PWROK"] = "0 В";
            if(choose == 2)
                board.Data["PLTRST"] = "0 В";
            break;
            case "Выход из строя часового кварцевового резонатора":
                board.Data["ClockSin"] = "0";
            break;
            case "Выход из строя BIOS":
                board.Data["BIOSSin"] = "0";
            break;
            case "Основной ШИМ контроллер процессора неисправен":
                board.Data["FiveV"] = "24 Ом";
                board.Data["VCCinAUX"] = "430 Ом ";
                board.Data["VCore"] = "0 В";
            break;
            case "Контроллер RT7296F не исправен":
                board.Data["VCCinAUX"] = "430 Ом";
                board.Data["VCore"] = "0 В";
                board.Data["AUXEnable"] = "0,44 КОм";
                board.Data["RT7296F_PG"] = "0,85 В";
            break;
            case "ШИМ-контроллер фазы питания AUX не исправен":
                board.Data["FiveV"] = "10 Ом";
                board.Data["VCCinAUX"] = "430 Ом ";
                board.Data["PWMAUX"] = "0";
            break;
            case "Сбитые SMD":
                board.Data["SMD"] = "0";
            break;
            case "Поврежденный сокет":
                board.Data["SMD"] = "1";
                usedIndexes.Add(0);
                Areas[0]= new MagnifierAreaData { x = 255, y = 195, ImagePath = "badSocket" };
            break;
            case "Вентилятор не работает":
            break;
            case "Molex порт не исправен":
                board.Data["Molex"] = "10В 5В";
            break;
        }
        InitAreas();
    }

    private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show(); 
            Close();
        }

    private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            var viewWindow = new ViewWindow();
            viewWindow.Show(); 
            Close();
        }
    private void checkAnswer(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный RadioButton
            RadioButton selectedRadioButton = choosePanel.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);

            if (selectedRadioButton != null)
            {
                // Получаем содержимое выбранной радиокнопки
                string selectedAnswer = selectedRadioButton.Content.ToString();

                // Правильный ответ
                string correctAnswer = board.Mulfunction; // Замените на свой правильный ответ

                // Сравниваем
                if (selectedAnswer == correctAnswer)
                {
                    // Ответ правильный
                    answerResult answerResultWindow = new answerResult(true);
                    answerResultWindow.ShowDialog();
                    Close();
                }
                else
                {
                    // Ответ неправильный
                    answerResult answerResultWindow = new answerResult(false);
                    answerResultWindow.ShowDialog();
                    Close();
                }
            }
            else
            {
                // Никакой вариант ответа не выбран
                MessageBox.Show("Выберите вариант ответа!");

            }
        }

private void showFan(object sender, RoutedEventArgs e){
 if(!isFanOn){
    multimetrButtonImage.Source = new BitmapImage(new Uri("images/cross_icon.png", UriKind.Relative));
    mainImage.Source = new BitmapImage(new Uri("images/goodFan.png", UriKind.Relative));
    mainImage.Width = 400;
    if(board.Mulfunction=="Вентилятор не работает"|| board.Mulfunction=="Molex порт не исправен")
        mainImage.Source = new BitmapImage(new Uri("images/badFan.png", UriKind.Relative));
    }else{
     fanButtonImage.Source = new BitmapImage(new Uri("images/fan_icon.png", UriKind.Relative));
     mainImage.Source = new BitmapImage(new Uri("images/bg.jpg", UriKind.Relative));
     mainImage.Width = 500;
     this.Content = mainCanvas;
    }
     isFanOn = !isFanOn;
}





public class MultimetrAreaData
{
    public int x { get; set; }
    public int y { get; set; }
    public string D { get; set; }
}
private void showAreasMultimetr(object sender, RoutedEventArgs e){
  if(!isMultimetrOn){
     MultimetrAreaData[] Areas = new MultimetrAreaData[12];
    Areas[0] = new MultimetrAreaData{x = 340, y = 460, D = "CMOS"};
    Areas[1] = new MultimetrAreaData{x = 215, y = 490, D = "USB1"};
    Areas[2] = new MultimetrAreaData{x = 195, y = 490, D = "USB2"};
    Areas[3] = new MultimetrAreaData{x = 365, y = 295, D = "PLTRST"};
    Areas[4] = new MultimetrAreaData{x = 325, y = 355, D = "SYS_PWROK"};
    Areas[5] = new MultimetrAreaData{x = 230, y = 270, D = "RT7296F_PG"};
    Areas[6] = new MultimetrAreaData{x = 115, y = 200, D = "AUXEnable"};
    Areas[7] = new MultimetrAreaData{x = 370, y = 150, D = "FiveV"};
    Areas[8] = new MultimetrAreaData{x = 105, y = 195, D = "VCCinAUX"};
    Areas[9] = new MultimetrAreaData{x = 90, y = 215, D = "VCore"};
    Areas[10] = new MultimetrAreaData{x = 370, y = 195, D = "Molex"};
    Areas[11] = new MultimetrAreaData{x = 150, y = 215, D = "null"};
     for(int i=0;i<11;i++){
    Image multimetrImage = new Image();
    TextBlock multimetrText = new TextBlock();
    multimetrText.Text = Areas[i].D;
    multimetrText.FontWeight = FontWeights.Bold;
    multimetrText.Foreground = Brushes.LightSkyBlue;
    multimetrText.FontStretch = FontStretches.UltraExpanded;
    multimetrImage.Source = new BitmapImage(new Uri("images/magnifier_areas.png", UriKind.Relative));
    multimetrImage.Width = 20;
    multimetrImage.MouseLeftButtonDown += multimetrAreaClick;
    multimetrImage.Name = Areas[i].D;
    Canvas.SetLeft(multimetrText, Areas[i].x-20+90); 
    Canvas.SetTop(multimetrText, Areas[i].y-60-Areas[i+1].y/15+55);
    Canvas.SetLeft(multimetrImage, Areas[i].x-20+90); 
    Canvas.SetTop(multimetrImage, Areas[i].y-60+55);  
    multimetr_areas.Children.Add(multimetrText);
    multimetr_areas.Children.Add(multimetrImage);
    multimetr_areas.Visibility = System.Windows.Visibility.Visible;
    multimetrButtonImage.Source = new BitmapImage(new Uri("images/cross_icon.png", UriKind.Relative));
    this.Content = mainCanvas;
    }
    }else{
     multimetrButtonImage.Source = new BitmapImage(new Uri("images/multimetr_icon.png", UriKind.Relative));
     mainImage.Source = new BitmapImage(new Uri("images/bg.jpg", UriKind.Relative));
     multimetr_areas.Visibility = System.Windows.Visibility.Hidden;
     this.Content = mainCanvas;
    }
     isMultimetrOn = !isMultimetrOn;
     }

private void multimetrAreaClick(object sender, RoutedEventArgs e){
    outputBox.Background = null;
    outputBox.Width = Double.NaN;
    outputBox.Height= Double.NaN;
    Image s = (Image)sender;
   int paragraphCount = outputBox.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Length;
    if(paragraphCount>10)
         outputBox.Text="";
         if(s.Name == "FiveV")
            outputBox.Text += "5V line "+" "+board.Data[s.Name]+"\n";
        else
            outputBox.Text += s.Name+" "+board.Data[s.Name]+"\n";
}
public class OsciAreaData
{
    public int x { get; set; }
    public int y { get; set; }
    public string D { get; set; }
}
private void showAreasOsci(object sender, RoutedEventArgs e){
        if(!isOsciOn){
     OsciAreaData[] Areas = new OsciAreaData[11];
    Areas[0] = new OsciAreaData{x = 340, y = 460, D = "ClockSin"};
    Areas[1] = new OsciAreaData{x = 355, y = 500, D = "BIOSSin"};
    Areas[2] = new OsciAreaData{x = 120, y = 200, D = "PWMAUX"};
    Areas[3] = new OsciAreaData{x = 240, y = 450, D = "null"};
     for(int i=0;i<3;i++){
    Image osciImage = new Image();
    TextBlock osciText = new TextBlock();
    osciText.Text = Areas[i].D;
    osciText.FontWeight = FontWeights.Bold;
    osciText.Foreground = Brushes.Lime;
    osciText.FontStretch = FontStretches.UltraExpanded;
    osciImage.Source = new BitmapImage(new Uri("images/magnifier_areas.png", UriKind.Relative));
    osciImage.Width = 20;
    osciImage.MouseLeftButtonDown += osciAreaClick;
    osciImage.Name = Areas[i].D;
    Canvas.SetLeft(osciText, Areas[i].x-20); 
    Canvas.SetTop(osciText, Areas[i].y-60-Areas[i+1].y/15);
    Canvas.SetLeft(osciImage, Areas[i].x-20); 
    Canvas.SetTop(osciImage, Areas[i].y-60);  
    osci_areas.Children.Add(osciText);
    osci_areas.Children.Add(osciImage);
    osci_areas.Visibility = System.Windows.Visibility.Visible;
    osciButtonImage.Source = new BitmapImage(new Uri("images/cross_icon.png", UriKind.Relative));
    this.Content = mainCanvas;
    }
    }else{
     osciButtonImage.Source = new BitmapImage(new Uri("images/oscilloscope_icon.png", UriKind.Relative));
     mainImage.Source = new BitmapImage(new Uri("images/bg.jpg", UriKind.Relative));
     osci_areas.Visibility = System.Windows.Visibility.Hidden;
     this.Content = mainCanvas;
    }
     isOsciOn = !isOsciOn;
}
private void osciAreaClick(object sender, RoutedEventArgs e){
    Image s = (Image)sender;
   int paragraphCount = outputBox.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Length;
         outputBox.Text="";
    ImageBrush img = new ImageBrush();
    if(board.Data[s.Name]=="1"){
    img.ImageSource =new BitmapImage(new Uri("images/goodSin.png", UriKind.Relative));
    }
    if(board.Data[s.Name]=="0"){
    img.ImageSource =new BitmapImage(new Uri("images/badSin.jpg", UriKind.Relative));
    }
    outputBox.Width = 400;
    outputBox.Height = 200;
    outputBox.Background = img;

}





public class MagnifierAreaData
{
    public int x { get; set; }
    public int y { get; set; }
    public string ImagePath { get; set; }
}

public MagnifierAreaData GetRandomArea(MagnifierAreaData[] Areas)
{


    Random rnd = new Random();
    int index = 0;
        do
        {
            index = rnd.Next(0, Areas.Length);
        } while (usedIndexes.Contains(index)); 
        usedIndexes.Add(index); 
        return Areas[index];
}

public void InitAreas()
{
 MagnifierAreaData[] goodAreas = new MagnifierAreaData[5];
    goodAreas[0] = new MagnifierAreaData { x = 245, y = 195, ImagePath = "a" };
    goodAreas[1] = new MagnifierAreaData { x = 395, y = 530, ImagePath = "b" };
    goodAreas[2] = new MagnifierAreaData { x = 320, y = 515, ImagePath = "c" };
    goodAreas[3] = new MagnifierAreaData { x = 150, y = 85, ImagePath = "d" };
    goodAreas[4] = new MagnifierAreaData { x = 360, y = 510, ImagePath = "f" };
    MagnifierAreaData[] badAreas = new MagnifierAreaData[4];
    badAreas[0] = new MagnifierAreaData { x = 80, y = 350, ImagePath = "ax" };
    badAreas[1] = new MagnifierAreaData { x = 255, y = 310, ImagePath = "bx" };
    badAreas[2] = new MagnifierAreaData { x = 106, y = 490, ImagePath = "cx" };
    badAreas[3] = new MagnifierAreaData { x = 334, y = 404, ImagePath = "dx" };
    if(board.Mulfunction == "Сбитые SMD"){
        Areas[0]=GetRandomArea(badAreas);
    for(int i=1;i<5;i++)
        Areas[i]=GetRandomArea(goodAreas);
    }else{
        if(board.Mulfunction != "Поврежденный сокет")
             Areas[0]=GetRandomArea(goodAreas);
        
    for(int i=1;i<5;i++)
        Areas[i]=GetRandomArea(goodAreas);
    }
}

  private void areasMagnifier(){
    for(int i=0;i<5;i++){
    Image magnifierImage = new Image();
    magnifierImage.Source = new BitmapImage(new Uri("images/magnifier_areas.png", UriKind.Relative));
    magnifierImage.MouseLeftButtonDown+=magnifierAreaClick;
    magnifierImage.Name=Areas[i].ImagePath.ToString();
    Canvas.SetLeft(magnifierImage, Areas[i].x-20+50); 
    Canvas.SetTop(magnifierImage, Areas[i].y-60);  
    magnifier_areas.Children.Add(magnifierImage);
    }
  }
  private void magnifierAreaClick(object sender, RoutedEventArgs e){
        outputBox.Background = null;
        outputBox.Width = Double.NaN;
        outputBox.Height= Double.NaN;
       Image s=(Image)sender;
       string imagePath="images/"+s.Name+".png";
       magnifier_areas.Visibility = System.Windows.Visibility.Hidden;
       mainImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

  }
  private void showAreasMagnifier(object sender, RoutedEventArgs e){
    if(!isMagnifierOn){
    areasMagnifier();
    magnifier_areas.Visibility = System.Windows.Visibility.Visible;
    magnifierButtonImage.Source = new BitmapImage(new Uri("images/cross_icon.png", UriKind.Relative));
    this.Content = mainCanvas;
    }else{
     magnifierButtonImage.Source = new BitmapImage(new Uri("images/search_icon.png", UriKind.Relative));
     mainImage.Source = new BitmapImage(new Uri("images/bg.jpg", UriKind.Relative));
     magnifier_areas.Visibility = System.Windows.Visibility.Hidden;
     this.Content = mainCanvas;
    }
     isMagnifierOn = !isMagnifierOn;
  }



}

