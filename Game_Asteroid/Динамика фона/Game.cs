using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Task_1
{
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] _objs;
        public static Bullet _bullet;
        public static Asteroid[] _asteroids;

        static Game()
        {
        }

        /// <summary>
        /// Запуск игры
        /// </summary>
        /// <param name="form">К какой форме привязываемся</param>
        public static void Init(Form form)
            {
                // Графическое устройство для вывода графики
                Graphics g;
                // предоставляет доступ к главному буферу графического контекста для текущего приложения
                _context = BufferedGraphicsManager.Current;
                g = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой
                                          // Запоминаем размеры формы
                Width = form.Width;
                Height = form.Height;
                // Связываем буфер в памяти с графическим объектом.
                // для того, чтобы рисовать в буфере
                Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

                //Load();

                Timer timer = new Timer { Interval = 100 };
                timer.Start();
                timer.Tick += Timer_Tick;
            }

        /// <summary>
        /// Таймер для прорисовки и обновления
        /// </summary>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Прорисовываем объекты
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid obj in _asteroids)
                obj.Draw();
            _bullet.Draw();
            Buffer.Render();
        }
        
        /// <summary>
        /// Обновляем все объекты
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    a.Power = 0;
                    _bullet.Power = 0;
                }
            }
            _bullet.Update();
        }

        /// <summary>
        /// Создаем массив из 191 объекта и далее запускаем их со сдвигом позиции
        /// </summary>
        public virtual void Load()
        {
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[3];
            _objs = new BaseObject[191];
            Random rnd1 = new Random(DateTime.Now.Millisecond);
            Random rnd2 = new Random(DateTime.Now.Millisecond + 1);
            int dir_X, dir_Y;

            for (int i = 0; i < 30; i++)
            {
                _objs[i] = new Square(new Point(i * 5, i * 5), new Point(20, 20), new Size(1, 1));
            }
            for (int i = 30; i < 190; i++)
            {
                dir_X = rnd1.Next(-20, 20);
                dir_Y = rnd2.Next(-20, 20);               
                _objs[i] = new Rain(new Point(Game.Width / 2, Game.Height / 2), new Point(dir_X, dir_Y), new Size(1, 1));

            }
            for (int i = 190; i < 191; i++)
            {
                _objs[i] = new Planet(new Point(300, 300), new Point(rnd1.Next(1, 10), rnd2.Next(1, 10)), new Size(50, 50));
            }

            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd1.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(600, rnd1.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }
        }

    }
}
