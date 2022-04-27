using QLHS.DB;
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
using System.Windows.Shapes;

namespace QLHS.Views.Regulation
{
    /// <summary>
    /// Interaction logic for frm_UpdateClass.xaml
    /// </summary>
    public partial class frm_UpdateClass : Window
    {
        private ClassManageDAO classManageDao = new ClassManageDAO();
        private RegulationDAO regulationDao = new RegulationDAO();
        RegulationMange regulationPage = new RegulationMange();

        public frm_UpdateClass(RegulationMange regulationPage)
        {
            InitializeComponent();
            try
            {
                this.regulationPage = regulationPage;
                cbGrade.ItemsSource = classManageDao.GetInfoGrade();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        private void cbGrade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                txtClassName.Text = "";
                cbClass.ItemsSource = classManageDao.GetInfoClass((int)cbGrade.SelectedValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void cbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbClass.SelectedValue != null)
            {
                try
                {
                    var infoClass = cbClass.SelectedItem as tb_Class;
                    txtClassName.Text = infoClass.Name;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbClass.SelectedValue != null)
                {
                    if (!String.IsNullOrEmpty(txtClassName.Text))
                    {
                        if (classManageDao.GetInfoClassByName(txtClassName.Text.Trim()) == null)
                        {
                            if (MessageBox.Show("Bạn có muốn cập nhật thông tin lớp học này?", "Cập nhật lớp học", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                var infoClass = cbClass.SelectedItem as tb_Class;
                                if (classManageDao.UpdateInfoNameClass(infoClass.ID, txtClassName.Text.ToString().Trim()))
                                {
                                    MessageBox.Show("Cập nhật lớp học thành công", "Cập nhật lớp học", MessageBoxButton.OK, MessageBoxImage.Information);
                                    regulationPage.txtQuantityClass.Text = regulationDao.getQuantityClass().ToString();
                                }
                                else
                                {
                                    MessageBox.Show("Có lỗi xảy ra, cập nhật thất bại", "Cập nhật lớp học", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tên lớp học đã tồn tại!", "Thêm lớp học", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên lớp học không được để trống", "Thêm lớp học", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn lớp học", "Cập nhật lớp học", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbClass.SelectedValue != null)
                {
                    if (MessageBox.Show("Bạn có muốn xóa thông tin lớp học này?", "Xóa lớp học", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var infoClass = cbClass.SelectedItem as tb_Class;
                        if (classManageDao.DeleteInfoClass(infoClass.ID))
                        {
                            MessageBox.Show("Xóa lớp học thành công", "Xóa lớp học", MessageBoxButton.OK, MessageBoxImage.Information);
                            cbClass.ItemsSource = classManageDao.GetInfoClass((int)cbGrade.SelectedValue);
                            txtClassName.Text = "";
                            regulationPage.txtQuantityClass.Text = regulationDao.getQuantityClass().ToString();
                        }
                        else
                        {
                            MessageBox.Show("Có lỗi xảy ra, xóa thất bại", "Xóa lớp học", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn lớp học muốn xóa", "Cập nhật lớp học", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
