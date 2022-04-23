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
                    MessageBox.Show("Sỉ số lớp đã đầy");
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin danh sách học sinh tìm kiếm
           
        }


        private void cbGrade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
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
                    lvStudent.ItemsSource = classManageDao.GetInfoStudentsOfClass((int)cbClass.SelectedValue);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
           
        }
    }
}
