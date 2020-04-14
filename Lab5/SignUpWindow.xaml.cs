using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Lab5
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        string connectionString = @"Data Source= iths.database.windows.net; Database=Group1; User Id=Group1sa; Password= Group1Password!;";
        string Email;
        bool signUpB = false;
        string ErrorMessage = "";
        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            if (PhoneTxt.Text.Length !=10)
            {
                ErrorMessage = "Skriv in giltig Telefonnummer\n";
                if (!VaildEmail.IsValidEmail(Email))
                {
                    ErrorMessage = ErrorMessage + "Skriv in giltig e-postadress\n";
                }
                if (PasswordTxt.Password.Length < 6)
                {
                    ErrorMessage = ErrorMessage + "Lösenordet är för kort (minst 6 tecken)";
                }
            }
            else if (!VaildEmail.IsValidEmail(Email))
            {
                ErrorMessage = "Skriv in giltig e-postadress\n";
                if (PasswordTxt.Password.Length < 6)
                {
                    ErrorMessage = ErrorMessage + "Lösenordet är för kort (minst 6 tecken)";
                }
            }
            else if (PasswordTxt.Password.Length < 6)
            {
                ErrorMessage = "Lösenordet är för kort (minst 6 tecken)";
            }
            else
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("personAdd", sqlCon);
                    sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@FirstName", FirstNameTxt.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@LastName", LastNameTxt.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Phonenumber", PhoneTxt.Text);
                    sqlCmd.Parameters.AddWithValue("@Email", EmailTxt.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Password", PasswordTxt.Password);
                    sqlCmd.Parameters.AddWithValue("@Access", "user");
                    sqlCmd.ExecuteNonQuery();
                    signUpB = true;
                    // Registration successful message
                    MessageBox.Show("successfull");
                    this.Visibility = Visibility.Hidden;
                }
            }
            if (!signUpB)
            {
                MessageBox.Show(ErrorMessage, "fel");
                ErrorMessage = "";
            }
        }

        private void EmailTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Email = this.EmailTxt.Text;
        }
    }
}
