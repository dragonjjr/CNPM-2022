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
using QLHS.Views.Class;
using QLHS.Views.Regulation;
using QLHS.Views.Grades;
using QLHS.Views.Report;

namespace QLHS.Views
{
    /// <summary>
    /// Interaction logic for Navigation.xaml
    /// </summary>
    public partial class Navigation : Window
    {
        public Navigation()
        {
            InitializeComponent();
            Main.Content = new StudentManage();
        }

        private void btnManageClass_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClassManager();
        }

        private void btnRegulationManage_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new RegulationMange();
        }

        private void btnStudentManage_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new StudentManage();
        }

        private void btnGradesManage_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new GradesManagement();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ReportManagement();
        }
    }
}
