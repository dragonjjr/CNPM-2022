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
            lvStudent.ItemsSource = classManageDao.GetInfoStudentsWithoutClass();
        }

        public frm_AddStudent()
        {
            InitializeComponent();

            lvStudent.ItemsSource = classManageDao.GetInfoStudentsWithoutClass();
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
                    if (MessageBox.Show("Bạn có muốn thêm học sinh này vào lớp", "Add student", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {

                        int classId = (int)classManagePage.cbClass.SelectedValue;
                        var student = lvStudent.SelectedItem as tb_Students;

                        if(classManageDao.AddStudentIntoClass(classId,student.ID))
                        {
                            MessageBox.Show("Thêm học sinh thành công", "Add student");

                            // Load lại danh sách
                            lvStudent.ItemsSource = classManageDao.GetInfoStudentsWithoutClass();
                            this.classManagePage.lvStudent.ItemsSource = classManageDao.GetInfoStudentsOfClass(classId);
                        }
                        else
                        {
                            MessageBox.Show("Có lỗi xảy ra, thêm thất bại", "Add student");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Sỉ số lớp đã đầy");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
