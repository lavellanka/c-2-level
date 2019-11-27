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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace App_CompanyEmployees
{
    /// <summary>
    /// Окно общего списка работников
    /// </summary>
    public partial class Win_Employees : Window
    {
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataTable dt;

        public Win_Employees()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Lesson7;Integrated Security=True";

            connection = new SqlConnection(connectionString);
            adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT Employees.ID, Employees.SecondName as 'Фамилия', Employees.FirstName as 'Имя', Employees.Salary as 'Зарплата', Employees.Position as 'Должность', Departments.Name as 'Отдел', Departments.ID as 'ID отдела' FROM Employees INNER JOIN Departments ON Employees.Department = Departments.ID", connection);
            adapter.SelectCommand = command;

            //insert
            command = new SqlCommand(@"INSERT INTO Employees (FirstName, SecondName, Salary, Position, Department) VALUES (@FirstName, @SecondName, @Salary, @Position, @DepartmentID); SET @ID = @@IDENTITY;", connection);
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50, "Имя");
            command.Parameters.Add("@SecondName", SqlDbType.NVarChar, 50, "Фамилия");
            command.Parameters.Add("@Salary", SqlDbType.Int, 0, "Зарплата");
            command.Parameters.Add("@Position", SqlDbType.NVarChar, 100, "Должность");
            command.Parameters.Add("@DepartmentID", SqlDbType.Int, 0, "ID отдела");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            adapter.InsertCommand = command;

            //update
            command = new SqlCommand(@"UPDATE Employees SET FirstName = @FirstName, SecondName = @SecondName, Salary = @Salary, Position = @Position, Department = @DepartmentID WHERE ID = @ID", connection);
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50, "Имя");
            command.Parameters.Add("@SecondName", SqlDbType.NVarChar, 50, "Фамилия");
            command.Parameters.Add("@Salary", SqlDbType.Int, 0, "Зарплата");
            command.Parameters.Add("@Position", SqlDbType.NVarChar, 100, "Должность");
            command.Parameters.Add("@DepartmentID", SqlDbType.Int, 0, "ID отдела");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapter.UpdateCommand = command;

            //delete
            command = new SqlCommand("DELETE FROM Employees WHERE ID = @ID", connection);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapter.DeleteCommand = command;

            dt = new DataTable();
            adapter.Fill(dt);
            lst_Employees.DataContext = dt.DefaultView;
        }

        /// <summary>
        /// Кнопка "Добавить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = dt.NewRow();
            Win_EditEmployee editWindow = new Win_EditEmployee (newRow);
            editWindow.Owner = this;
            editWindow.ShowDialog();
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                dt.Rows.Add(editWindow.resultRow);
                adapter.Update(dt);
            }
        }

        /// <summary>
        /// Кнопка "Удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Del_Click(object sender, RoutedEventArgs e)
        {
            if (lst_Employees.SelectedItem != null)
            {
                MessageBoxResult confirm = MessageBox.Show("Вы уверены, что хотите удалить работника?", "Подтвердите удаление", MessageBoxButton.YesNo);
                if (confirm == MessageBoxResult.Yes)
                {
                    DataRowView newRow = (DataRowView)lst_Employees.SelectedItem;
                    newRow.Row.Delete();
                    adapter.Update(dt);
                }
            }
            else MessageBox.Show("Для удаления выберите работника в списке");
        }

        /// <summary>
        /// Кнопка "Редактировать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (lst_Employees.SelectedItem != null)
            {
                DataRowView newRow = (DataRowView)lst_Employees.SelectedItem;
                newRow.BeginEdit();
                Win_EditEmployee editWindow = new Win_EditEmployee(newRow.Row);
                editWindow.Owner = this;
                editWindow.ShowDialog();
                if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
                {
                    newRow.EndEdit();
                    adapter.Update(dt);
                }
                else
                {
                    newRow.CancelEdit();
                }
            }
            else MessageBox.Show("Для редактирования выберите департамент в списке");
        }

        /// <summary>
        /// Кнопка "К списку департаментов" - открывает окно со списком департаментов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DepartmentList_Click(object sender, RoutedEventArgs e)
        {
            Win_Departments win_Departments = new Win_Departments();
            win_Departments.Show();
            this.Close();
        }
    }
}
