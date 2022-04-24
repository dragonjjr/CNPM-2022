using QLHS.Model;
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
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserName.Text;
            string password = pwbPassword.Password;
            try
            {
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
    }
}
