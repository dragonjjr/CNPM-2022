﻿using System;
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

namespace QLHS.Views.Class
{
    /// <summary>
    /// Interaction logic for ClassManager.xaml
    /// </summary>
    public partial class ClassManager : Page
    {
        public ClassManager()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Open add form.
            frm_AddStudent form = new frm_AddStudent();
            form.Show();
            
        }
    }
}
