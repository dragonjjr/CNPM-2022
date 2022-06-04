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
            this.DataContext = new UserRegister();
        }

         private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string username = txtUserName.Text.Trim();
            string password = pwbPassword.Password.Trim();
            string confirmpass = pwbConfirmPass.Password.Trim();

            if (email.Length > 0 && username.Length > 0 && password.Length > 0 && confirmpass.Length > 0)
            {
                if (Validation.GetHasError(txtEmail) ==false  && Validation.GetHasError(txtUserName) == false && Validation.GetHasError(pwbPassword) == false && Validation.GetHasError(pwbConfirmPass) == false)
                {
                    try
                    {
                        // Gọi hàm đăng ký
                        int Result = authenticationDAO.Register(email ,username, password, confirmpass);

                        switch (Result)
                        {
                            case 0:
                                MessageBox.Show("Tên tài khoản đã tồn tại");
                                txtUserName.Focus();
                                break;
                            case 1:
                                MessageBox.Show("Đăng ký thành công");

                                txtEmail.Clear();
                                Validation.ClearInvalid(txtEmail.GetBindingExpression(TextBox.TextProperty));

                                txtUserName.Clear();
                                Validation.ClearInvalid(txtUserName.GetBindingExpression(TextBox.TextProperty));

                                pwbPassword.Clear();
                                Validation.ClearInvalid(pwbPassword.GetBindingExpression(PasswordBoxHelper.PasswordBoxAssistant.BoundPassword));

                                pwbConfirmPass.Clear();
                                Validation.ClearInvalid(pwbConfirmPass.GetBindingExpression(PasswordBoxHelper.PasswordBoxAssistant.BoundPassword));


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
                else
                {
                    if(Validation.GetHasError(txtEmail))
                    {
                        txtEmail.Focus();
                    }
                    else if (Validation.GetHasError(txtUserName))
                    {
                        txtUserName.Focus();
                    }
                    else if (Validation.GetHasError(pwbPassword))
                    {
                        pwbPassword.Focus();
                    }
                    else if (Validation.GetHasError(pwbConfirmPass))
                    {
                        pwbConfirmPass.Focus();
                    }
                }
            }
            else
            {
                if(txtEmail.Text.Length < 1)
                {
                    txtEmail.Text = "";
                    txtEmail.Focus();
                }
                else if (txtUserName.Text.Length < 1)
                {
                    txtUserName.Text = "";
                    txtUserName.Focus();
                }
                else if (pwbPassword.Password.Length < 1)
                {
                    pwbPassword.Password = " ";
                    pwbPassword.Clear();
                    pwbPassword.Focus();
                }
                else
                {
                    pwbConfirmPass.Password = " ";
                    pwbConfirmPass.Clear();
                    pwbConfirmPass.Focus();
                }
            }
        }
    }

    public class UserRegister
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

    }
}
