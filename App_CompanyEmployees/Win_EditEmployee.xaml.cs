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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace App_CompanyEmployees
{
    /// <summary>
    /// Логика взаимодействия для Win_EditEmployee.xaml
    /// </summary>
    public partial class Win_EditEmployee : Window
    {
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataTable dt;

        public DataRow resultRow { get; set; }

        public Win_EditEmployee(DataRow dataRow)
        {
            InitializeComponent();
            resultRow = dataRow;
        }

        /// <summary>
        /// Загрузка окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Lesson7;Integrated Security=True");
            adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT ID, Name FROM Departments", connection);
            adapter.SelectCommand = command;

            dt = new DataTable();
            adapter.Fill(dt);
            DepartmentCmbBox.ItemsSource = dt.DefaultView;

            FirstNameTxtBx.Text = resultRow["Имя"].ToString();
            SecondNameTxtBx.Text = resultRow["Фамилия"].ToString();
            SalaryTxtBx.Text = resultRow["Зарплата"].ToString();
            PositionTxtBx.Text = resultRow["Должность"].ToString();
            DepartmentCmbBox.Text = resultRow["Отдел"].ToString();
            Lbl_DepartmentID.Content = dt.Rows[DepartmentCmbBox.SelectedIndex][0];
        }

        /// <summary>
        /// Событие при изменение выбора в ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Lbl_DepartmentID.Content = dt.Rows[DepartmentCmbBox.SelectedIndex][0];
        }

        /// <summary>
        /// кнопка "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(SalaryTxtBx.Text, out int salary))
            {
                resultRow["Имя"] = FirstNameTxtBx.Text;
                resultRow["Фамилия"] = SecondNameTxtBx.Text;
                resultRow["Зарплата"] = salary;
                resultRow["Должность"] = PositionTxtBx.Text;
                resultRow["ID отдела"] = Lbl_DepartmentID.Content;
                resultRow["Отдел"] = DepartmentCmbBox.Text;
                DialogResult = true;
            }
            else MessageBox.Show("Неккоректные данные в поле \"Зарплата\"");
        }

        /// <summary>
        /// Кнопка "Отмена"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
