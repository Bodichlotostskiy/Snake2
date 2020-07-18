using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeConsoleApp;
using SnakeConsoleApp.menu;
using SnakeConsoleApp.Enums;
using SnakeConsoleApp.Factory;
using SnakeConsoleApp.Helpers;
using SnakeConsoleApp.Installers;

namespace SnakeConsoleApp.menu
{
    public class MenuLevel
    {

        delegate void method();
        string[] _menuItems2;
        int _counter = 0;
        public MenuLevel()
        {
            MenuTwo();
        }
        public MenuLevel(string[] menuItems)
        {

            _menuItems2 = menuItems;
        }
        void FirstLevle()
        {
            
            MenuSelection newMenu = new MenuSelection(1);
        }
        void SecondLevel()
        {

            MenuSelection newMenu = new MenuSelection(2);
        }
        void ThirdLevel()
        {

            MenuSelection newMenu = new MenuSelection(3);
        }
        
        public void MenuTwo()
        {
            string[] items2 = { "FIRST", "SECOND", "THIRD" };
            method[] methods2 = new method[] { FirstLevle, SecondLevel, ThirdLevel };
            MenuLevel menu = new MenuLevel(items2);
            int menuResult2;
            do
            {
                menuResult2 = menu.DrawMenuLevel();
                methods2[menuResult2]();
            } while (menuResult2 != items2.Length - 1);

        }
        public int DrawMenuLevel()
        {   
            ConsoleKeyInfo key2;
            do
            {

                Console.Clear();
                for (int i = 0; i < _menuItems2.Length; i++)
                {

                    Console.SetCursorPosition(30, 5 + i);
                    if (_counter == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(_menuItems2[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.WriteLine(_menuItems2[i]);
                }
                key2 = Console.ReadKey();
                if (key2.Key == ConsoleKey.UpArrow)
                {
                    _counter--;
                    if (_counter == -1) _counter = _menuItems2.Length - 1;
                }
                if (key2.Key == ConsoleKey.DownArrow)
                {
                    _counter++;
                    if (_counter == _menuItems2.Length) _counter = 0;
                }

            }
            while (key2.Key != ConsoleKey.Enter);



            return _counter;
        }
    }
}
