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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        private AuthenticationDAO authenticationDAO = new AuthenticationDAO();
        public Register()
        {
            InitializeComponent();
        }

         private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserName.Text;
            string password = pwbPassword.Password;
            string confirmpass = pwbConfirmPass.Password;

            try
            {
                // Gọi hàm đăng ký
                int Result = authenticationDAO.Register(username, password, confirmpass);

                switch (Result)
                {
                    case 0:
                        MessageBox.Show("Tên tài khoản đã tồn tại");
                        txtUserName.Focus();
                        break;
                    case 1:
                        MessageBox.Show("Đăng ký thành công");
                        txtUserName.Text = "";
                        pwbPassword.Password = "";
                        pwbConfirmPass.Password = "";
                        break;
                    case 2:
                        MessageBox.Show("Mật khẩu xác nhận lại không đúng");
                        pwbConfirmPass.Focus();
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
