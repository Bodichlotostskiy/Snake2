using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SnakeConsoleApp;
using SnakeConsoleApp.Enums;
using SnakeConsoleApp.Factory;
using SnakeConsoleApp.Helpers;
using SnakeConsoleApp.Installers;
using SnakeConsoleApp.menu;
using SnakeConsoleApp.UserService;

namespace SnakeConsoleApp.menu
{
    public class MenuSelection
    {

        delegate void method();
        string[] _menuItems;
        int _counter = 0;
        int _levelSelect=100;
        public MenuSelection()
        {
            Menu();
        }
        public MenuSelection(int level)
        {

            Console.Clear();
            switch (level)
            {
                case 1:
                    Console.SetCursorPosition(30, 5);
                    Console.WriteLine("LEVEL 1");
                    _levelSelect = 150;
                    break;
                case 2:
                    Console.SetCursorPosition(30, 5);
                    Console.WriteLine("LEVEL 2");
                    _levelSelect = 100;
                    break;
                case 3:
                    Console.SetCursorPosition(30, 5);
                    Console.WriteLine("LEVEL 3");
                    _levelSelect = 80;
                    break;

            }
            Thread.Sleep(2000);

            Menu();



        }
        public MenuSelection(string[] menuItems)
        {
            _menuItems = menuItems;
        }
        public static void StartGame()
        {
        }



        public  void Start()
        {
           // if (user == null)
               // user = new User();

            int score = 0;
            Console.Clear();
            LineInstaller line = new LineInstaller();
            line.DrawShapes();
            Point food = FoodFactory.GetRandomFood(119, 20, '+');
            Console.ForegroundColor = ColorHelper.GetRandomColor(new Random().Next(1, 5));
            food.DrawPoint();
            Console.ResetColor();
            Snake snake = new Snake();
            snake.CreateSnake(5, new Point(5, 5, 'z'), DirectionEnum.Right);
            snake.DrawLine();

            ScoreHelper.GetScore(score);
            while (true)
            {
                if (line.Collision(snake) || snake.CollisionWithOwnTail())
                {
                    break;
                }
                if (snake.Eat(food))
                {
                    score++;
                    ScoreHelper.GetScore(score);

                    food = FoodFactory.GetRandomFood(119, 20, '+');
                    Console.ForegroundColor = ColorHelper.GetRandomColor(new Random().Next(1, 5));
                    food.DrawPoint();
                    Console.ResetColor();
                }

                Thread.Sleep(_levelSelect);
                snake.Move();

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.PressKey(key.Key);
                }
            }

            Concede();
            Console.ReadKey();
        }
        void Exit()
        {
            Environment.Exit(0);
        }
         void Level()
        {
            Console.Clear();
            MenuLevel menu = new MenuLevel();
            

        //    var temp = int.TryParse(Console.ReadLine(), out _levelSelect);//(0-максимум)(можна по аналогии зделать к примеру 3-4 скорости как в меню но времени у меня на ето не хватает)
        }
         public void Menu()
        {
            string[] items = { "START", "LEVEL", "EXIT" };
            method[] methods = new method[] { Start, Level,Exit  };
            MenuSelection menu = new MenuSelection(items);
            int menuResult;
            do
            {
                menuResult = menu.DrawMenu();
                methods[menuResult]();
            } while (menuResult != items.Length - 1);
        }

        public void Concede()
        {
            Console.Clear();
            Console.WriteLine("Game over");
            Console.WriteLine("To try again press Enter");
            Console.WriteLine("Back to menu press Backspace");
        }



        public int DrawMenu()
        {
            ConsoleKeyInfo key;
            do
            {

                Console.Clear();

                for (int i = 0; i < _menuItems.Length; i++)
                {

                    Console.SetCursorPosition(30, 5+i);
                    if (_counter == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(_menuItems[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.WriteLine(_menuItems[i]);
                }
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        _counter--;
                        if (_counter == -1) _counter = _menuItems.Length - 1;
                    }
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        _counter++;
                        if (_counter == _menuItems.Length) _counter = 0;
                    }
                
            }
            while (key.Key != ConsoleKey.Enter);
            
            
            
            return _counter;
        }
    }
}
