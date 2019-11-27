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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

//  Ольга Назарова
//  Задание: Изменить WPF-приложение для ведения списка сотрудников компании (из урока 5), используя связывание данных, DataGrid и ADO.NET.

namespace App_CompanyEmployees
{
    /// <summary>
    /// Окно списка департаментов
    /// </summary>
    public partial class Win_Departments : Window
    {
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataTable dt;

        public Win_Departments()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Win_DepatrmentList_Loaded(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Lesson7;Integrated Security=True");
            adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT ID, Name FROM Departments", connection);
            adapter.SelectCommand = command;

            //insert
            command = new SqlCommand(@"INSERT INTO Departments (Name) VALUES (@Name); SET @ID = @@IDENTITY;", connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            adapter.InsertCommand = command;

            //update
            command = new SqlCommand(@"UPDATE Departments SET Name = @Name WHERE ID = @ID", connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapter.UpdateCommand = command;

            //delete
            command = new SqlCommand("DELETE FROM Departments WHERE ID = @ID", connection);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapter.DeleteCommand = command;

            dt = new DataTable();
            adapter.Fill(dt);
            lst_Departments.DataContext = dt.DefaultView;
        }

        /// <summary>
        /// Кнопка "Добавить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = dt.NewRow();
            Win_EditDepartment editWindow = new Win_EditDepartment(newRow);
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
            if (lst_Departments.SelectedItem != null)
            {
                try
                {
                    MessageBoxResult confirm = MessageBox.Show("Вы уверены, что хотите удалить департамент?", "Подтвердите удаление", MessageBoxButton.YesNo);
                    if (confirm == MessageBoxResult.Yes)
                    {
                        DataRowView newRow = (DataRowView)lst_Departments.SelectedItem;
                        newRow.Row.Delete();
                        adapter.Update(dt);
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Ошибка удаления департамента - в департаменте числятся работники");
                }
                
            }
            else MessageBox.Show("Для удаления выберите департамент в списке");
        }

        /// <summary>
        /// Кнопка "Редактировать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (lst_Departments.SelectedItem != null)
            {
                DataRowView newRow = (DataRowView)lst_Departments.SelectedItem;
                newRow.BeginEdit();
                Win_EditDepartment editWindow = new Win_EditDepartment(newRow.Row);
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
        /// Кнопка "К общему списку сотрудников" - открывает окно с общим списком сотрудников
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_GenListEmpl_Click(object sender, RoutedEventArgs e)
        {
            Win_Employees win_Employees = new Win_Employees();
            win_Employees.Show();
            this.Close();
        }
    }
}
