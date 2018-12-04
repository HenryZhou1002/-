using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  游戏规则:
  1.如果玩家A踩到玩家B,玩家B退6格
  2.踩到地雷,退6格.
  3.踩到幸运轮盘:1.交换位置,2.轰炸对方,使对方退6格
  4.踩到暂停,暂停一个回合.
  5.踩到方块,什么也不干.
*/





namespace 飞行棋项目
{
    class Program
    {
        static int[] Maps = new int[100];  //用静态字段，来使得其被所有方法访问到。
        //声明一个静态数组，来存两个玩家的坐标。
        static int[] PlayerPos = new int[2];  //1.玩家A的坐标，2.玩家B的坐标

        static string[] PlayerNames = new string[2]; //存储两个玩家的姓名.

        static bool[] Flags = new bool[2];  //玩家是否踩到暂停的标志,默认是false.Flags[0],Flags[1]
        static void Main(string[] args)
        {
            GameShow();
            InputPlayerNames();

            //姓名输入完后,首先清屏幕,调用游戏头
            Console.Clear();
            GameShow();
            Console.WriteLine("{0}的士兵用A表示",PlayerNames[0]);
            Console.WriteLine("{0}的士兵用B表示",PlayerNames[1]);

            //在画地图之前,首先应该初始化地图
            InitialMap();
            DrawMap();


            while (PlayerPos[0] < 99 && PlayerPos[1] < 99)//当玩家A和B都不在终点时,不停地玩游戏.
            {

                if(Flags[0]==false)
                {
                    PlayGame(0);
                }
                else
                {
                    Flags[0] = false;  //暂停一回合后,将bool置为false;
                }

                if (PlayerPos[0]>=99)//结束游戏
                {
                    Console.WriteLine("玩家{0}赢了",PlayerNames[0]);
                    break;
                }


                if (Flags[1]==false)
                {
                    PlayGame(1);
                }
                else
                {
                    Flags[1] = false;  //暂停一回合后,将bool置为false;
                }
                if (PlayerPos[1] >= 99)//结束游戏
                {
                    Console.WriteLine("玩家{0}赢了", PlayerNames[1]);
                    break;
                }

            }//while  游戏结束.

            //调用胜利标志

            win();


            Console.ReadKey();  //把原本的控制台的后续内容去掉
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
        public static void InputPlayerNames()
        {
            Console.WriteLine("请输入玩家A的姓名:");
            PlayerNames[0] = Console.ReadLine();
            while (PlayerNames[0] == "")
            {
                Console.WriteLine("玩家A的姓名不能为空,请重新输入:");
                PlayerNames[0] = Console.ReadLine();
            }

            Console.WriteLine("请输入玩家B的姓名:");
            PlayerNames[1] = Console.ReadLine();
            while (PlayerNames[1]=="" || PlayerNames[1]==PlayerNames[0])
            {
                if (PlayerNames[1]=="")
                {
                    Console.WriteLine("玩家B的姓名不能为空,请重新输入:");
                    PlayerNames[1] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("玩家B的姓名不能与玩家A的姓名相同,请重新输入:");
                    PlayerNames[1] = Console.ReadLine();
                }
            }

        }
        public static void DrawMap()//画地图
        {
            Console.WriteLine("图例:幸运轮盘:◎   地雷:☆   暂停:▲   时空隧道:卐");
            #region 画第一行
            //先画第一行0-29
            //也就是第一横行
            for (int i=0; i<30;i++)
            {
                //如果玩家A和玩家B的坐标相同，画一个"<>"
                //限定A和B在地图内,不能出地图.如果出了地图要特殊处理.
                //玩家A和玩家B都在地图的第一行上,才能画"<>"
                Console.Write(DrawStringMap(i));

            }
            #endregion

            #region 画第一列
            // 画完第一横行之后,换行
            Console.WriteLine();
            // 画第一个竖列
            // 由于Console.WriteLine()只能换行并从左开始,所以,需要画过个空格29x2 个空格.
            for (int i = 30 ; i < 35; i++ )//第一竖行,30-34
            {
                for(int j = 0; j <= 28; j++)    //画空格
                {
                    Console.Write("  ");
                }
                Console.Write(DrawStringMap(i));
                Console.WriteLine();
            }
            #endregion

            #region 画第二行
            for (int i = 64; i >= 35; i--)
            {               
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();

            #endregion

            #region 画二列
            
            for (int i = 65; i < 70; i++)
            {
                Console.Write(DrawStringMap(i));
                Console.WriteLine();
            }


            #endregion

            #region 画第三行,也就是最后一行
            for (int i = 70; i <= 99; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();

            #endregion
        }//DrawMap()方法的结尾
        #region  DrawStringMap(i)从画地图的方法中抽象出方法
        public static string DrawStringMap(int i)
        {
            string str = "";
            //当玩家A与B坐标相同的时候，在地图上画<>
            //当玩家A和B不同的时候，该怎么画怎么画。
            if (PlayerPos[0] == PlayerPos[1] && PlayerPos[1] == i)// 判断玩家B在第一行上
            {
                str = "<>";
            }
            else if (PlayerPos[0] == i)  //表面玩家A在第一行,但是A和B坐标不同
            {
                //控制台上,两个半角占的位置,等于一个全角.
                //shift+ 空格 画全角
                str = "Ａ";
            }
            else if (PlayerPos[1] == i)
            {
                str = "Ｂ";
            }
            else // 画固定的关卡
            {
                switch (Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        str = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Green;
                        str = "◎";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Red;
                        str = "★";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        str = "▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        str = "卍";
                        break;
                }
            }
            return str;
        }
        #endregion
        public static void PlayGame(int playerNumber)
        {
            Random r = new Random();
            int rNumber = r.Next(1, 7);

            Console.WriteLine("{0}按任意键开始扔骰子", PlayerNames[playerNumber]);
            Console.ReadKey(true);

            Console.WriteLine("{0}扔出了{1}", PlayerNames[playerNumber], rNumber);
            PlayerPos[playerNumber] += rNumber;  //根据随机的值,前进4格.
            ChangePos();   //每次坐标发生改变,都需要调用ChangePos()函数,来限定坐标在0-99之间.
            Console.ReadKey(true);

            Console.WriteLine("{0}按任意键开始行动", PlayerNames[playerNumber]);
            Console.ReadKey();

            //1.玩家A踩到玩家B,踩到方块,幸运轮盘,暂停,时空隧道...
            if (PlayerPos[playerNumber] == PlayerPos[1- playerNumber])
            {
                //玩家A踩到玩家B
                Console.WriteLine("玩家{0}踩到了玩家{1},玩家{2}退6格", PlayerNames[playerNumber], PlayerNames[1- playerNumber], PlayerNames[playerNumber]);
                PlayerPos[1- playerNumber] -= 6;
                ChangePos();   //每次坐标发生改变,都需要调用ChangePos()函数,来限定坐标在0-99之间.
                Console.ReadKey();
            }
            else
            {
                //踩到了关卡
                int postion = Maps[PlayerPos[playerNumber]];//关卡的信息,值为:0,1,2,3,4


                #region switch
                switch (postion)
                {
                    case 0:
                        Console.WriteLine("玩家{0}踩到了方块,安全.", PlayerNames[playerNumber]);
                        Console.ReadKey();
                        break;
                    case 1:
                        Console.WriteLine("玩家{0}踩到了幸运轮盘.",PlayerNames[playerNumber]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                //玩家0和玩家1交换位置
                                Console.WriteLine("玩家{0}和玩家{1}交换位置", PlayerNames[playerNumber], PlayerNames[1- playerNumber]);
                                Console.ReadKey(true);
                                //交换位置
                                int temp = PlayerPos[playerNumber];
                                PlayerPos[playerNumber] = PlayerPos[1- playerNumber];
                                PlayerPos[1- playerNumber] = temp;
                                Console.WriteLine("交换完成! 按任意键,继续游戏.");
                                break;
                            }
                            else if (input == "2")
                            {
                                //玩家2后退6步
                                Console.WriteLine("玩家{0}选择轰炸玩家{1},玩家{2}退6格", PlayerNames[playerNumber], PlayerNames[1- playerNumber], PlayerNames[playerNumber]);
                                Console.ReadKey(true);
                                PlayerPos[1] -= 6;
                                ChangePos();   //每次坐标发生改变,都需要调用ChangePos()函数,来限定坐标在0-99之间.
                                Console.WriteLine("玩家{0}退了6格", PlayerNames[1- playerNumber]);
                                Console.ReadKey(true);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("只能输入0 或者1   1.交换位置. 2.轰炸对方");
                                input = Console.ReadLine();
                            }
                        }
                        break;

                    case 2://踩到地雷,退6格
                        Console.WriteLine("玩家{0}踩到了地雷,退6格", PlayerNames[playerNumber]);
                        Console.ReadKey();
                        PlayerPos[0] -= 6;
                        ChangePos();   //每次坐标发生改变,都需要调用ChangePos()函数,来限定坐标在0-99之间.
                        break;

                    case 3: //踩到了暂停.逻辑很复杂,最后写.
                        Console.WriteLine("玩家{0}踩到了暂停,暂停一个回合", PlayerNames[playerNumber]);
                        Flags[playerNumber] = true;

                        Console.ReadKey(true);
                        break;

                    case 4: //时空隧道
                        Console.WriteLine("玩家{0}踩到了时空隧道,前进10格", PlayerNames[playerNumber]);
                        PlayerPos[0] += 10;
                        ChangePos();   //每次坐标发生改变,都需要调用ChangePos()函数,来限定坐标在0-99之间.
                        Console.ReadKey(true);
                        break;
                }
                #endregion

            }//else

            Console.Clear();
            DrawMap();
        }

        public static void win()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                         ");
            Console.WriteLine("                                         ");
            Console.WriteLine("                       胜利              ");

        }

        public static void ChangePos()   //限定玩家坐标要在0-99之间,当玩家坐标发生改变的时候调用.
        {
            if(PlayerPos[0] < 0)
            {
                PlayerPos[0] = 0;
            }
            if (PlayerPos[0]>=99)
            {
                PlayerPos[0] = 99;
            }

            if (PlayerPos[1] < 0)
            {
                PlayerPos[1] = 0;
            }
            if (PlayerPos[1] >= 99)
            {
                PlayerPos[1] = 99;
            }
        }
    }
}
