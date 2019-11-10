using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


//Свиргстина А.А.
//+ 1. Добавить космический корабль, как описано в методичке - добавила, в т.ч. движение вправо, влево
// 2. Доработать игру «Астероиды»:
//а) Добавить ведение журнала в консоль с помощью делегатов;
// б) * добавить это и в файл.
// 3. Разработать аптечки, которые добавляют энергию - класс Kit
//4. Добавить подсчет очков за сбитые астероиды - поле Total

namespace Task_1
{
    class Program
    {
        private static Button Start_btn = new Button();
        private static Button Exit_btn = new Button();
        private static Label Author_lbl = new Label();
        private static Button Record_btn = new Button();
        private static Form form = new Form
        {
            Width = Screen.PrimaryScreen.Bounds.Width,
            Height = Screen.PrimaryScreen.Bounds.Height
        };

        static void Main(string[] args)
        {
            Author_lbl.Text = "Автор: Свиргстина А.А.";
            Author_lbl.BackColor = Color.Black;
            Author_lbl.ForeColor = Color.White;
            Author_lbl.Size = new Size(120, 20);
            Author_lbl.Location = new Point(260, 12);
            form.Controls.Add(Author_lbl);

            Start_btn.Text = "Начало игры";
            Start_btn.Size = new Size(100, 20);
            Start_btn.Location = new Point(10, 10);
            form.Controls.Add(Start_btn);
            Start_btn.Click += new System.EventHandler(Start_btn_Click);

            Exit_btn.Text = "Выход";
            Exit_btn.Size = new Size(60, 20);
            Exit_btn.Location = new Point(190, 10);
            form.Controls.Add(Exit_btn);
            Exit_btn.Click += new System.EventHandler(Exit_btn_Click);

            Record_btn.Text = "Рекорды";
            Record_btn.Size = new Size(60, 20);
            Record_btn.Location = new Point(120, 10);
            form.Controls.Add(Record_btn);

            form.Show();

            SplashScreen splash = new SplashScreen();
            splash.Init(form);
            splash.Load();
            SplashScreen.Draw();
            Application.Run(form);
        }
        private static void Start_btn_Click(object sender, EventArgs e)
        {
            Start_btn.Visible = false;
            Exit_btn.Visible = false;
            Author_lbl.Visible = false;
            Record_btn.Visible = false;

            Game game = new Game();
            game.Init(form);
            game.Load();
            Game.Draw();
        }

        private static void Exit_btn_Click(object sender, EventArgs e)
        {
            form.Close();
        }
    }
}