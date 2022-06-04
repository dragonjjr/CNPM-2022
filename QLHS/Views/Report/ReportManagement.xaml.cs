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

namespace QLHS.Views.Report
{
    /// <summary>
    /// Interaction logic for ReportManagement.xaml
    /// </summary>
    public partial class ReportManagement : Page
    {
        private GradesManagementDAO gradesManagementDAO = new GradesManagementDAO();
        private SubjectDAO subjectDAO = new SubjectDAO();
        private ReportDAO reportDAO = new ReportDAO();
        public ReportManagement()
        {
            InitializeComponent();
            List<LoaiBaoCao> dt = new List<LoaiBaoCao>();
            dt.Add(new LoaiBaoCao
            {
                ID = 1,
                Name = "Báo cáo tổng kết môn"
            });
            dt.Add(new LoaiBaoCao
            {
                ID = 2,
                Name = "Báo cáo tổng kết học kỳ"
            });
            cbReport.ItemsSource = dt;
            cbSemeter.ItemsSource = gradesManagementDAO.GetInfoSemeters();
            cbSubject.ItemsSource = subjectDAO.GetInfoSubjects();
        }
        private void cbReport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbReport.SelectedValue != null)
            {
                if (cbReport.SelectedValue.ToString() == "1")
                {
                    txtTitle.Text = "Báo cáo tổng kết môn";
                    cbSubject.Visibility = Visibility.Visible;
                    lblcbSubject.Visibility = Visibility.Visible;
                }
                else if (cbReport.SelectedValue.ToString() == "2")
                {
                    txtTitle.Text = "Báo cáo tổng kết học kỳ";
                    cbSubject.Visibility = Visibility.Collapsed;
                    lblcbSubject.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (cbReport.SelectedValue != null)
            {
                if(cbReport.SelectedValue.ToString() == "1")
                {
                    if(cbSubject.SelectedValue != null && cbSemeter.SelectedValue != null)
                    {
                        var list = reportDAO.BaoCaoTongKetMon((int)cbSubject.SelectedValue, (int)cbSemeter.SelectedValue);
                        if(list.Count() >0)
                        {
                            lvReport.ItemsSource = list;
                        }
                        else
                        {
                            lvReport.ItemsSource = null;
                            MessageBox.Show("Không tìm thấy dữ liệu", "Báo cáo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn môn học và học kỳ", "Báo cáo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    if (cbSemeter.SelectedValue != null)
                    {
                        var list = reportDAO.BaoCaoTongKetHocKy((int)cbSemeter.SelectedValue);
                        if (list.Count() > 0)
                        {
                            lvReport.ItemsSource = list;
                        }
                        else
                        {
                            lvReport.ItemsSource = list;
                            MessageBox.Show("Không tìm thấy dữ liệu", "Báo cáo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn học kỳ", "Báo cáo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại báo cáo", "Báo cáo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

public class LoaiBaoCao
{
    public int ID { get; set; }
    public string Name { get; set; }
}