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
using System.Xml;
using System.Net.Http;

namespace Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void GoToLogInButton_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow logInWindow = new LogInWindow();
            logInWindow.ShowDialog();
            //logInWindow.Topmost = true;

        }

        private void ContactInfoButton_Click(object sender, RoutedEventArgs e)
        {
            KontaktWindow kont = new KontaktWindow();
            kont.ShowDialog();
        }
        static string CallAPI(Uri u)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = client.GetAsync(u).Result;
                if (result.IsSuccessStatusCode)
                {
                    response = result.Content.ReadAsStringAsync().Result;
                }
            }
            return response;
        }
        private void mainWindowLoaded(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("http://www.yr.no/sted/Sverige/Jämtland/Åre/varsel.xml");
            string xmlData = CallAPI(uri);
            
            string xPathForecast = "/weatherdata/forecast/tabular/time/temperature/@value";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            
            string degrees = xmlDoc.SelectSingleNode(xPathForecast).Value;
            lblWeather.Content = $"Temperatur: {degrees} grader";
        }

        private void Adminbtn_Click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            adminPage.ShowDialog();
            //logInWindow.Topmost = true;
        }

        private void AdminButtonClicked(object sender, RoutedEventArgs e)
        
            {
                AdminPage adminPage = new AdminPage();
                adminPage.ShowDialog();
                //logInWindow.Topmost = true;
            }
        
    }
}
