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
using System.Data;

namespace App_CompanyEmployees
{
    /// <summary>
    /// Логика взаимодействия для Win_EditDepartment.xaml
    /// </summary>
    public partial class Win_EditDepartment : Window
    {
        public DataRow resultRow { get; set; }

        public Win_EditDepartment(DataRow dataRow)
        {
            InitializeComponent();
            resultRow = dataRow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NameTextBox.Text = resultRow["Name"].ToString();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            resultRow["Name"] = NameTextBox.Text;
            DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
