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
using QLHS.DB;

namespace QLHS.Views.Class
{
    /// <summary>
    /// Interaction logic for frm_AddStudent.xaml
    /// </summary>
    public partial class frm_AddStudent : Window
    {
        private ClassManageDAO classManageDao = new ClassManageDAO();
        ClassManager classManagePage = new ClassManager();

        public frm_AddStudent(ClassManager classManagePage)
        {

            InitializeComponent();
            this.classManagePage = classManagePage;
            lvStudent.ItemsSource = classManageDao.GetInfoStudentsWithoutClass("");
        }

        public frm_AddStudent()
        {
            InitializeComponent();

            lvStudent.ItemsSource = classManageDao.GetInfoStudentsWithoutClass("");
        }

        private void SelectCurrentItem(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddIntoClass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check quantity
                if (classManageDao.CheckQuantityStudentOfclass(1, 1))
                {
                    var classInfo = classManagePage.cbClass.SelectedItem as tb_Class;
                    var student = lvStudent.SelectedItem as tb_Students;

                    if (MessageBox.Show("Bạn có muốn thêm học sinh này vào lớp "+classInfo.Name.ToString(), "Thêm học sinh", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {

                        if (classManageDao.AddStudentIntoClass(classInfo.ID, student.ID))
                        {

                            // Load lại danh sách
                            lvStudent.ItemsSource = classManageDao.GetInfoStudentsWithoutClass(txtSearch.Text);
                            this.classManagePage.lvStudent.ItemsSource = classManageDao.GetInfoStudentsOfClass(classInfo.ID,this.classManagePage.txtSearch.Text);

                            MessageBox.Show("Thêm học sinh thành công", "Thêm học sinh", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Có lỗi xảy ra, thêm thất bại", "Thêm học sinh", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Sỉ số lớp đã đầy", "Thêm học sinh",MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DoSearch()
        {
            var listStudent = classManageDao.GetInfoStudentsWithoutClass(txtSearch.Text);
            if (listStudent.Count() > 0)
            {
                lvStudent.ItemsSource = listStudent;
            }
            else
            {
                MessageBox.Show("Không tìm thấy học sinh", "Tìm kiếm học sinh", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DoSearch();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
