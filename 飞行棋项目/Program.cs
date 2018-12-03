using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞行棋项目
{
    class Program
    {
        static void Main(string[] args)
        {
            GameShow();
        }

        public static void GameShow()//画游戏头
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("**************************");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("**************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**************************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("******.Net飞行棋游戏******");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("**************************");

        }

    }
}
