using PersonalList.Classes; // подключаем пространство имен с классами в папке
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
using System.Collections;
using System.Collections.ObjectModel;


//Свиргстина Александра
// 1. Создать сущности Employee и Department и заполните списки сущностей начальными данными.
//2. Для списка сотрудников и списка департаментов предусмотреть визуализацию(отображение). 
//  Это можно сделать, например, с использованием ComboBox или ListView.
//3. Предусмотреть возможность редактирования сотрудников и департаментов.
// Должна быть возможность изменить департамент у сотрудника. 
//  Список департаментов для выбора, можно выводить в ComboBox, это все можно выводить на дополнительной форме.
//4. Предусмотреть возможность создания новых сотрудников и департаментов.
//Реализовать данную возможность либо на форме редактирования, либо сделать новую форму.

namespace PersonalList
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewEmployee, IViewDepartment
    {
        PresenterEmployee e;
        PresenterDepartment d;

        public MainWindow()
        {
            InitializeComponent();
            e = new PresenterEmployee(this);
            d = new PresenterDepartment(this);

            btnNextE.Click += delegate { e.Next(); };
            btnPrevE.Click += delegate { e.Prev(); };
            btnAddE.Click += delegate { e.Add(); };
            btnRemoveE.Click += delegate { e.Remove(); };
            btnAlterE.Click += delegate { e.Alter(); };


            btnAddD.Click += delegate { d.Add(); };
            btnRemoveD.Click += delegate { d.Remove(); };
            btnAlterD.Click += delegate { d.Alter(); };

            this.Closing += delegate { e.SaveAEmployeeList(); };
            this.Loaded += delegate { e.LoadAEmployeeList(); };
            this.Closing += delegate { d.SaveADepartmentList(); };
            this.Loaded += delegate { d.LoadADepartmentList(); };

            DepartmentGrid.DataContext = DepartmentListD; // пока не привязал БД к гриду и не сделал биндинг к конкретной коллекции внутри класса, обновление так и не заработало
            EmployeeGrid.DataContext = EmployeeList;
        }

        public string Surname { get => txtSurnameE.Text; set => txtSurnameE.Text = value; }
        public string Name { get => txtNameE.Text; set => txtNameE.Text = value; }
        public string DepartmentE { get => cmbDepartmentE.Text; set => cmbDepartmentE.Text = value; }
        public AEmployeeList EmployeeList { get => e.CurrentEmployeeList; set => e.CurrentEmployeeList = value; }

        public string NewSurname { get => txtSurnameNew.Text; set => txtSurnameNew.Text = value; }
        public string NewName { get => txtNameNew.Text; set => txtNameNew.Text = value; }
        public string NewDepartment { get => cmbDepartmentNew.Text; set => cmbDepartmentNew.Text = value; }

        public string DepartmentD
        {
            get
            {
                if (d.CurrentIndex<0) //чтобы переменная не ссылалась неизвестно куда при пустой форме
                {
                    return null;
                }
                else
                {
                    return cmbDepartmentD.SelectedValue.ToString();
                }
            }
                set => cmbDepartmentD.Text = value;
        }
        public string DepartmentNew { get => txtDepartmentNew.Text; set => txtDepartmentNew.Text = value; }
        public ADepartmentList DepartmentListD { get => d.CurrentDepartamentList; set => d.CurrentDepartamentList=value; } 

    }
}

