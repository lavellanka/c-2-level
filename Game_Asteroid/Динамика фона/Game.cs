using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

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
        public static Kit[] _kits;
        public static Ship _ship; 
        private static Timer _timer = new Timer { Interval = 100 };
        
        /// <summary>
        /// Метод для делегата
        /// </summary>
        /// <param name="message">сообщение для записи в журнал</param>
        static void PublicMessageConsole(string message)
        {
            Console.WriteLine(DateTime.Now + " " + message);
        }
        /// <summary>
        /// Метод для делегата
        /// </summary>
        /// <param name="message"></param>
        static void PublicMessageFile(string message)
        {
            FileStream fs = new FileStream("log.txt", FileMode.Append, FileAccess.Write);
            StreamWriter fStream = new StreamWriter(fs, Encoding.Default);
            fStream.WriteLine(DateTime.Now + " " + message);
            fStream.Close();
        }

        static Game()
        {
        }

        /// <summary>
        /// Запуск игры
        /// </summary>
        /// <param name="form">К какой форме привязываемся</param>
        public virtual void Init(Form form)
        {
            // объявляем делегат, присваиваем ему 2 метода, в 72 строке используем
            Action<string> Target = PublicMessageConsole;
            Target += PublicMessageFile;

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
            
            _timer.Start();
            // пример записи в журнал через делегаты (без условий, просто сначала через один метод, потом другой)
            Target("Игра запущена");

            _timer.Tick += Timer_Tick;
            Ship.MessageDie += Finish;

            // делаем так, чтобы игра реагировала на нажатия клавиш
            form.KeyPreview = true;
            form.KeyDown += Form_KeyDown;
        }

        /// <summary>
        /// Конец игры
        /// </summary>
        public static void Finish()
        {
            // объявляем делегат, присваиваем ему 2 метода, в 96 строке используем
            Action<string> Target = PublicMessageConsole;
            Target += PublicMessageFile;

            _timer.Stop();

            // пример записи в журнал через делегаты (без условий, просто сначала через один метод, потом другой)
 
            Target("Игра окончена");

            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }

        /// <summary>
        /// Управление кораблем
        /// </summary>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(20, 0), new Size(4, 1));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
            if (e.KeyCode == Keys.Left) _ship.Left();
            if (e.KeyCode == Keys.Right) _ship.Right();
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
            foreach (Asteroid a in _asteroids)
            {
                a?.Draw();
            }
            foreach (Kit k in _kits)
            {
                k?.Draw();
            }
            _bullet?.Draw();
            _ship?.Draw();
            if (_ship != null)
            {
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, Width - 90, 7);
                Buffer.Graphics.DrawString("Total:" + _ship.Total, SystemFonts.DefaultFont, Brushes.White, Width - 90, 25);
            }
                
            Buffer.Render();
        }
        
        /// <summary>
        /// Обновляем все объекты
        /// </summary>
        public static void Update()
        {
            // объявляем делегат, присваиваем ему 2 метода, ниже используем
            Action<string> Target = PublicMessageConsole;
            Target += PublicMessageFile;

            foreach (BaseObject obj in _objs) obj.Update();
            _bullet?.Update();
            _ship?.Update();
            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                if (_bullet != null && _bullet.Collision(_asteroids[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    // при столкновении пули и астериода оба погибают, тотал растет на величину пауэра астероида
                    _ship?.TotalUp(_asteroids[i].Power);
                    _asteroids[i] = null;
                    _bullet = null;

                    // пример записи в журнал через делегаты (без условий, просто сначала через один метод, потом другой)
                    Target("Попадание пули в астероид");

                    continue;
                }
                if (!_ship.Collision(_asteroids[i])) continue;
                var rnd = new Random();
                _ship?.EnergyLow(rnd.Next(1, 10)); // при столкновении астеорида с кораблем, у корабля падает энергия
                System.Media.SystemSounds.Asterisk.Play();

                // пример записи в журнал через делегаты (без условий, просто сначала через один метод, потом другой)
                Target("Столкновение корабаля с астероидом");

                if (_ship.Energy <= 0) _ship?.Die();
            }

            for (var i = 0; i < _kits.Length; i++)
            {
                if (_kits[i] == null) continue;
                _kits[i].Update();

                if (!_ship.Collision(_kits[i])) continue;
                _ship?.EnergyUp(_kits[i].Power); // при столкновении аптечки с кораблем, у корабля растет энергия
                System.Media.SystemSounds.Hand.Play();

                // пример записи в журнал через делегаты (без условий, просто сначала через один метод, потом другой)
                Target("Поймали аптечку");
            }
        }

        /// <summary>
        /// Создаем массив из 191 объекта и далее запускаем их со сдвигом позиции
        /// </summary>
        public virtual void Load()
        {
            _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10)); //добавляем корабль
            _bullet = new Bullet(new Point(10, 400), new Point(20, 0), new Size(4, 1)); //добавляем пулю
            _asteroids = new Asteroid[3]; //добавляем 3 астериода
            _kits = new Kit[2]; // добавялем 2 аптечки
            _objs = new BaseObject[191];
            
            // делаем 2 типа рандома, чтобы сгенерировать разные траектории полета
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

            for (var i = 0; i < _kits.Length; i++)
            {
                int r = rnd1.Next(15, 20);
                _kits[i] = new Kit(new Point(600, rnd1.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }
        }

    }
}
