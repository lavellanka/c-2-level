using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Task_1
{
    public partial class Form1 : Form
    {
        public Button Start_btn = new Button();
        public Button Record_btn = new Button();
        public Button Exit_btn = new Button();
        public Label Author_lbl = new Label();

        public Form1()
        {
            Start_btn.Text = "Начало игры";
            Start_btn.Size = new Size(100, 20);
            Start_btn.Location = new Point(10, 10);
            this.Controls.Add(Start_btn);

            Record_btn.Text = "Рекорды";
            Record_btn.Size = new Size(60, 20);
            Record_btn.Location = new Point(120, 10);
            this.Controls.Add(Record_btn);

            Exit_btn.Text = "Выход";
            Exit_btn.Size = new Size(60, 20);
            Exit_btn.Location = new Point(190, 10);
            this.Controls.Add(Exit_btn);

            Author_lbl.Text = "Автор: Свиргстина А.А.";
            Author_lbl.BackColor = Color.Black;
            Author_lbl.ForeColor = Color.White;
            Author_lbl.Size = new Size(120, 20);
            Author_lbl.Location = new Point(260, 12);
            this.Controls.Add(Author_lbl);
        }
    }
}
