using QLHS.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace QLHS.Views
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Login : Page
    {
        private AuthenticationDAO authenticationDAO = new AuthenticationDAO();
        public Login()
        {
            InitializeComponent();
            this.DataContext = new UserLogin();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = pwbPassword.Password.Trim();
            if (username.Length > 0 && password.Length > 0)
            {
                if (Validation.GetHasError(txtUserName) == false && Validation.GetHasError(pwbPassword) == false)
                {
                    try
                    {
                        //Gọi Hàm Login
                        int Result = authenticationDAO.Login(username, password);


                        switch (Result)
                        {
                            case 0:
                                MessageBox.Show("Tên tài khoản không tồn tại");
                                txtUserName.Focus();
                                break;
                            case 1:
                                Navigation form = new Navigation();
                                App.Current.MainWindow.Close();
                                form.Show();
                                break;
                            case 2:
                                MessageBox.Show("Sai Mật khẩu");
                                pwbPassword.Focus();
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
                else
                {
                    if (Validation.GetHasError(txtUserName))
                    {
                        txtUserName.Focus();
                    }
                    else if (Validation.GetHasError(pwbPassword))
                    {
                        pwbPassword.Focus();
                    }
                }
            }
            else
            {
                if (username.Length == 0)
                {
                    txtUserName.Text = "";
                    txtUserName.Focus();
                }
                else
                {
                    pwbPassword.Password = " ";
                    pwbPassword.Password = "";
                    pwbPassword.Focus();
                }
            }
        }
    }

    public class UserLogin
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
