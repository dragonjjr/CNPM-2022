using QLHS.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace QLHS.Views
{
    /// <summary>
    /// Interaction logic for ForgetPass.xaml
    /// </summary>
    public partial class ForgetPass : Page
    {
        private AuthenticationDAO authenticationDAO = new AuthenticationDAO();
        public class User
        {
            public string Email { get; set; }
            public string UserName { get; set; }

            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }
        public ForgetPass()
        {
            InitializeComponent();
            this.DataContext = new User();
        }

        private void btnGui_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string username = txtUserName.Text;

            if (email.Length > 0 && username.Length > 0)
            {
                if (Validation.GetHasError(txtEmail) == false && Validation.GetHasError(txtUserName) == false)
                {
                    try
                    {
                        bool Result = authenticationDAO.CheckUserForGetPass(email, username);

                        if (Result == true)
                        {
                            spResetPass.Visibility = Visibility.Visible;
                            spDisplayCheckInfor.Visibility = Visibility.Collapsed;
                        }
                        else if (Result == false)
                        {
                            MessageBox.Show("Thông tin tài khoản hoặc email không trùng khớp", "Quên mật khẩu", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi");
                    }
                }
                else
                {
                    if (Validation.GetHasError(txtEmail))
                    {
                        txtEmail.Focus();
                    }
                    else if (Validation.GetHasError(txtUserName))
                    {
                        txtUserName.Focus();
                    }
                }
            }
            else
            {
                if (txtEmail.Text.Length < 1)
                {
                    txtEmail.Text = "";
                    txtEmail.Focus();
                }
                else if (txtUserName.Text.Length < 1)
                {
                    txtUserName.Text = "";
                    txtUserName.Focus();
                }
            }
        }

        private void btnconfirm_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string username = txtUserName.Text;
            string password = pwbPassword.Password;
            string confirmpass = pwbConfirmPass.Password;
            if (password.Length > 0 && confirmpass.Length > 0)
            {
                if (Validation.GetHasError(pwbPassword) == false && Validation.GetHasError(pwbConfirmPass) == false)
                {
                    if (password == confirmpass)
                    {
                        try
                        {
                            bool Result = authenticationDAO.ResetPass(email, username, password);
                            if(Result == false)
                            {
                                MessageBox.Show("Đặt lại mật khẩu thành công", "Quên mật khẩu", MessageBoxButton.OK, MessageBoxImage.Information);

                                spDisplayCheckInfor.Visibility = Visibility.Visible;
                                spResetPass.Visibility = Visibility.Collapsed;

                                txtEmail.Clear();
                                Validation.ClearInvalid(txtEmail.GetBindingExpression(TextBox.TextProperty));

                                txtUserName.Clear();
                                Validation.ClearInvalid(txtUserName.GetBindingExpression(TextBox.TextProperty));

                                pwbPassword.Clear();
                                Validation.ClearInvalid(pwbPassword.GetBindingExpression(PasswordBoxHelper.PasswordBoxAssistant.BoundPassword));

                                pwbConfirmPass.Clear();
                                Validation.ClearInvalid(pwbConfirmPass.GetBindingExpression(PasswordBoxHelper.PasswordBoxAssistant.BoundPassword));

                            }
                            else
                            {
                                MessageBox.Show("Đặt lại mật khẩu không thành công", "Quên mật khẩu", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Lỗi");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu xác nhận lại không trùng khớp", "Quên mật khẩu", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    if (Validation.GetHasError(pwbPassword))
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
                if (pwbPassword.Password.Length < 1)
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
}
