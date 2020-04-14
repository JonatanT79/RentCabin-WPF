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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Lab5
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 

    
    
    public partial class AdminPage : Window
    {
        string connectionString = @"Data Source= iths.database.windows.net; Database=Group1; User Id=Group1sa; Password= Group1Password!;";

        public AdminPage()
        {
            InitializeComponent();
        }
        /*
        private void CabinBtn1_Clicked(object sender, MouseButtonEventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(connectionString);
            string query = "Select * from cabin Where Id = 1";
            string result = query;

            Stuga1.Content = result;
        }
        -->
        */
        private void BtnCabin1_Click(object sender, RoutedEventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source= iths.database.windows.net; Database=Group1; User Id=Group1sa; Password= Group1Password!;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();


            SqlCommand command;
            SqlDataReader dataReader;
            string sql, output = "";
            
            sql = "Select Id, FirstName, LastName, PhoneNumber, Email" +
                " from person";
                    

            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            while(dataReader.Read())
            { 
                {
                    output = output + dataReader.GetValue(0) + "\t" + "\t" + dataReader.GetValue(1) + "\t" + "\t" + dataReader.GetValue(2) + "\t" + dataReader.GetValue(3) + "\n";
                    
                }
            }
            Stuga1.Content = output;
        }

        private void DeleteId(object sender, RoutedEventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source= iths.database.windows.net; Database=Group1; User Id=Group1sa; Password= Group1Password!;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            string id = DeleteTxtBox.Text;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = "";

            sql = $"Delete person where Id ={id}";

            command = new SqlCommand(sql, cnn);

            adapter.DeleteCommand = new SqlCommand(sql, cnn);
            adapter.DeleteCommand.ExecuteNonQuery();

            command.Dispose();
            cnn.Close();


            AdminPage adminPage = new AdminPage();

        }

        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            string id = userTxtBox.Text;
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source= iths.database.windows.net; Database=Group1; User Id=Group1sa; Password= Group1Password!;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();


            SqlCommand command;
            SqlDataReader dataReader;
            string sql, output = "";

            sql = $"Select Id, FirstName, LastName, PhoneNumber, Email from person where Id = {id}";


            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                {
                    FirstnameTxt.Text = output + dataReader.GetValue(1);
                    LastnameTxt.Text = output + dataReader.GetValue(2);
                    PhoneText.Text = output + dataReader.GetValue(3);
                    MailTxt.Text = output + dataReader.GetValue(4);

                }
            }
            cnn.Close();
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            string id = userTxtBox.Text;
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source= iths.database.windows.net; Database=Group1; User Id=Group1sa; Password= Group1Password!;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();



            SqlCommand command;
            SqlCommand command1;
            SqlCommand command2;

            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = "";
            string sql1 = "";
            string sql2 = "";
            string sql3 = "";

            string firstName = FirstnameTxt.Text;
            string lastName = LastnameTxt.Text;
            string phone = PhoneText.Text;
            string mail = MailTxt.Text;

            
            sql = $"Update person set FirstName = '{firstName}' where Id = {id}";
            command = new SqlCommand(sql, cnn);
            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();
            command.Dispose();
            


            sql1 = $"Update person set LastName = '{lastName}' where Id = {id}";
            command1 = new SqlCommand(sql1, cnn);
            adapter.UpdateCommand = new SqlCommand(sql1, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();
            command.Dispose();
            

            sql2 = $"Update person set PhoneNumber = {phone} where Id = {id}";
            command2 = new SqlCommand(sql2, cnn);
            adapter.UpdateCommand = new SqlCommand(sql2, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();
            command.Dispose();

            sql3 = $"Update person set Email = '{mail}' where Id = {id}";
            command2 = new SqlCommand(sql3, cnn);
            adapter.UpdateCommand = new SqlCommand(sql3, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();
            command.Dispose();

            cnn.Close();
        }
    }
}
