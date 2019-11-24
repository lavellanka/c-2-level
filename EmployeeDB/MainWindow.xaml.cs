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

namespace EmployeeDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
     
        Presenter p;

        public MainWindow()
        {
            InitializeComponent();
            p = new Presenter(this);

            DepCombo.ItemsSource = p.ListOfDep;
            DepComboEdit.ItemsSource = p.ListOfDep;
            ButtonSave.IsEnabled = false;
            ButtonCancel.IsEnabled = false;
            ButtonDepAdd.IsEnabled = false;

            ButtonDepRem.Click += delegate { p.RemoveDepartment(); };
            ButtonDepEdit.Click += delegate { p.RenameDepartment(); };
            ButtonDepAdd.Click += delegate { p.AddDepartment(); };
        }
        
        private void DepCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepCombo.SelectedIndex>-1)
                ListEmployee.ItemsSource = p.EmployeeList;
        }
        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ListEmployee.SelectedIndex > -1)
            {
                p.SelectEmployee();
                ButtonSave.IsEnabled = true;
                ButtonCancel.IsEnabled = true;
                ButtonAdd.IsEnabled = false;
            }
        }
        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DepComboEdit.SelectedIndex > -1)
            {
                p.AddEmployee();  
            }
        }
        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (DepCombo.SelectedIndex>-1 && ListEmployee.SelectedIndex > -1)
            {
                p.RemoveEmployee();
            }
        }
        /// <summary>
        /// Сохранение отредактированного сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (DepComboEdit.SelectedIndex > -1)
            {
                if (p.SaveEmployee())
                    EndEdit();
            }

        }
        /// <summary>
        /// Отмена редактирования сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            EndEdit();
        }
        /// <summary>
        /// Завершение редактирования сотрудника
        /// </summary>
        private void EndEdit()
        {
            ButtonSave.IsEnabled = false;
            ButtonCancel.IsEnabled = false;
            ButtonAdd.IsEnabled = true;
        }

        private void DepText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DepText.Text.Count() > 0)
            {
                ButtonDepAdd.IsEnabled = true;
            }
            else
            {
                ButtonDepAdd.IsEnabled = false;
            }
        }

        /// Реализация интерфейса

        public string Name { get => NameText.Text; set => NameText.Text = value; }
        public string Surname { get => SurnameText.Text; set => SurnameText.Text = value; }
        public DateTime Birthday
        {
            get
            {
                DateTime dtParse;
                if (DateTime.TryParse(BirthdayText.Text, out dtParse))
                    return dtParse;
                else throw new FormatException("Недопустимое значение даты. Формат дд.мм.гггг или дд/мм/гг");
            }
            set => BirthdayText.Text = value.ToShortDateString();
        }
        public string Position { get => PosText.Text; set => PosText.Text = value; }
        public string DepName { get => DepText.Text; set => DepText.Text = value; }

        public Department CurrDep => (DepCombo.SelectedItem as Department);

        public Department EditDep
        {
            get => (DepComboEdit.SelectedItem as Department);
            set => DepComboEdit.SelectedItem = value;
        }

        public Employee CurrEmployee => ListEmployee.SelectedItem as Employee;
    }
}