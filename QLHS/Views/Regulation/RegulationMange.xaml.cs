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

namespace QLHS.Views.Regulation
{
    /// <summary>
    /// Interaction logic for RegulationMange.xaml
    /// </summary>
    public partial class RegulationMange : Page
    {
        public RegulationMange()
        {
            InitializeComponent();
        }

        private void popUpUpdateClass_Click(object sender, RoutedEventArgs e)
        {
            // Open add form.
            frm_UpdateClass form = new frm_UpdateClass();
            form.Show();
        }

        private void popUpAddClass_Click(object sender, RoutedEventArgs e)
        {
            // Open add form.
            frm_AddClass form = new frm_AddClass();
            form.Show();
        }

        private void popUpUpdateSubject_Click(object sender, RoutedEventArgs e)
        {
            // Open add form.
            frm_UpdateSubject form = new frm_UpdateSubject();
            form.Show();
        }

        private void popUpAddSubject_Click(object sender, RoutedEventArgs e)
        {
            // Open add form.
            frm_AddSubject form = new frm_AddSubject();
            form.Show();
        }
    }
}
