using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞行棋项目
{
    class Program
    {
        static int[] Maps = new int[100];  //用静态字段，来使得其被所有方法访问到。

        static void Main(string[] args)
        {

            InitialMap();
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

        public static void InitialMap()//初始化地图
        {
            int[] luckyturn = { 6, 23, 40, 55, 69, 83 }; //幸运轮盘
            for (int i = 0; i < luckyturn.Length; i++)
            {
                int index = luckyturn[i];
                Maps[index] = 1;
            }

            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//地雷
            for (int i = 0; i < landMine.Length; i++)
            {
                Maps[landMine[i]] = 2;

            }

            int[] pause = { 9, 27, 60, 93 }; //暂停
            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = 3;
            }

            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 }; //时空隧道
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Maps[timeTunnel[i]] = 4;
            }
            


        }

        public static void DrawMap()//画地图
        {

        }

    }
}
