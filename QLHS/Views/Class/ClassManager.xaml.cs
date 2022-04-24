using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using QLHS.Model;
using QLHS.DB;

namespace QLHS.Views.Class
{
    /// <summary>
    /// Interaction logic for ClassManager.xaml
    /// </summary>
    public partial class ClassManager : Page
    {
        private ClassManageDAO classManageDao = new ClassManageDAO();

        public ClassManager()
        {
            InitializeComponent();

            Init();

        }


        private void Init()
        {
            try
            {
                cbGrade.ItemsSource = classManageDao.GetInfoGrade();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        private void ResetForm()
        {
            txtSearch.Text = "";
            tbNumStudent.Text = "";
            lvStudent.ItemsSource = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cbClass.SelectedValue != null)
            {
                // Check quantity
                int classId = (int)cbClass.SelectedValue;
                if (classManageDao.CheckQuantityStudentOfclass(classId, 1))
                {
                    // Open add form.
                    frm_AddStudent form = new frm_AddStudent(this);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Sỉ số lớp đã đầy", "Thêm học sinh", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn lớp học!", "Thêm học sinh", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DoSearch()
        {
            var listStudent = classManageDao.GetInfoStudentsOfClass((int)cbClass.SelectedValue, txtSearch.Text);
            if (listStudent.Count() > 0)
            {
                lvStudent.ItemsSource = listStudent;
            }
            else
            {
                lvStudent.ItemsSource = null;
                MessageBox.Show("Không tìm thấy học sinh", "Tìm kiếm học sinh", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (cbClass.SelectedValue != null)
            {
                // Lấy thông tin danh sách học sinh tìm kiếm
                try
                {
                    DoSearch();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn lớp học!", "Thêm học sinh", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void cbGrade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ResetForm();
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
                    ResetForm();

                    var infoClass = cbClass.SelectedItem as tb_Class;
                    tbNumStudent.Text = infoClass.Quantity.ToString();
                    lvStudent.ItemsSource = classManageDao.GetInfoStudentsOfClass((int)cbClass.SelectedValue,"");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbClass.SelectedValue != null)
                {
                    if (lvStudent.SelectedItem != null)
                    {
                        var classInfo = cbClass.SelectedItem as tb_Class;
                        var student = lvStudent.SelectedItem as tb_Students;

                        if (MessageBox.Show("Bạn có muốn xóa học sinh này khỏi lớp " + classInfo.Name.ToString(), "Xóa học sinh", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            if (classManageDao.DeleteStudentFromClass(student.ID))
                            {

                                // Load lại danh sách
                                lvStudent.ItemsSource = classManageDao.GetInfoStudentsOfClass(classInfo.ID,txtSearch.Text);

                                MessageBox.Show("Xóa học sinh thành công", "Xóa học sinh", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Có lỗi xảy ra, xóa thất bại", "Xóa học sinh", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa chọn học sinh muốn xóa!", "Xóa học sinh", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn lớp học!", "Xóa học sinh", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
