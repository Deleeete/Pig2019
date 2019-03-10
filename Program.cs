using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Pig2019
{
    class Program
    {
        static void Main(string[] args)
        {
        Choose:
            if (args.Length == 0)
            {
                Console.Clear();
                Console.WriteLine("\n选择操作模式：\n*********************");
                Console.WriteLine("1.生猪\n2.喂猪\n3.杂交\n4.杀猪\n5.分析");
                string str = Console.ReadLine(); byte mode; Console.Clear();
                try { mode = Convert.ToByte(str); }
                catch { WriteError("\n非法输入，按任意键重输\n"); Console.ReadKey(); goto Choose; }
                if (mode == 1) { goto Born; }
                else if (mode == 2) { goto Feed; }
                else if (mode == 3) { goto Cross; }
                else if (mode == 4) { goto Murder; }
                else if (mode == 5) { goto Analysis; }
                else { WriteError("没有找到对应的选项，按任意键重输\n"); Console.ReadKey(); goto Choose; }
            }
            else if (args[0] == "1")
            {
                goto Born;
            }
            else if (args[0] == "2") { goto Feed; }
            else if (args[0] == "3") { goto Cross; }
            else if (args[0] == "4") { goto Murder; }
            else if (args[0] == "5") { goto Analysis; }
        Born:
            Console.Clear();
            Console.Title = "Pig2019-生产器";
            Console.WriteLine("生猪模式\n*********************");
            Console.WriteLine("\n输入种猪的名字：");
            string bornname = Console.ReadLine(); if (bornname == "##") { goto Choose; }
            if (File.Exists(bornname + ".pig"))
            {
                WriteError("同名猪已存在"); Console.WriteLine("按任意键重新输入"); Console.ReadKey(); goto Born;
            }
            Born(bornname);
            #region Confirm to Leave
            Console.WriteLine("继续生猪？(Y/n)");
            string i = Console.ReadLine();
            if (i == "y" | i == "Y")
            {
                goto Born;
            }
            else if (i == "n" | i == "N")
            {
                goto Choose;
            }
        #endregion
        Feed:
            Console.Clear();
            Console.Title = "Pig2019-喂猪器";
            Console.WriteLine("喂猪模式\n*****************");
            Console.WriteLine("\n输入需要喂的猪名：");
            string feedname = Console.ReadLine(); if (feedname == "##") { goto Choose; }
            bool ed = ExistCheck(feedname);
            if (!ed) { Console.ReadKey(); goto Feed; }
            WriteError("注意：喂猪会导致猪的体积快速增长，速度可接近硬盘的最大写入速度，是否真的要继续？（Y/n）");
            string choice = Console.ReadLine();
            if (choice == "Y" | choice == "y")
            {
                Feed(feedname);
            }
            else { goto Choose; }

            #region Confirm to Leave
            Console.WriteLine("继续喂猪？(Y/n)");
            string k = Console.ReadLine();
            if (k == "y" | k == "Y")
            {
                goto Feed;
            }
            else if (k == "n" | k == "N")
            {
                goto Choose;
            }
        #endregion
        Cross:
            Console.Clear();
            Console.Title = "Pig2019-杂交器";
            Console.WriteLine("连续杂交模式\n*****************");
            Console.WriteLine("\n输入两只亲本猪的名字，使用空格分隔：");
            string parents = Console.ReadLine();
            if (parents == "#") { goto Choose; }
            string[] prt;
            try
            {
                prt = parents.Split(' ');
                WriteInform("\n亲本1：" + prt[0] + "\n亲本2：" + prt[1] + "\n");
                bool eb = ExistCheck(prt[0]);
                if (!eb) { Console.ReadKey(); goto Cross; }
                bool ec = ExistCheck(prt[1]);
                if (!ec) { Console.ReadKey(); goto Cross; }
            }
            catch { WriteError("\n发生错误，检查分隔符是否正确。"); Console.WriteLine("按任意键重新输入..."); Console.ReadKey(); goto Cross; }
            Console.WriteLine("\n输入子代猪的名字：");
            string product = Console.ReadLine(); if (product == "##") { goto Choose; }
            Cross(prt[0], prt[1], product);
            Console.WriteLine("\n按任意键继续...");
            Console.ReadKey();
            goto Cross;

        Murder:
            Console.Clear();
            Console.Title = "Pig2019-杀猪器";
            Console.WriteLine("杀猪模式\n*****************");
            Console.WriteLine("\n输入想要谋杀的猪的名字：");
            string murdername = Console.ReadLine(); if (murdername == "##") { goto Choose; }
            bool ea = ExistCheck(murdername);
            if (!ea)
            { Console.ReadKey(); goto Murder; }
            File.Delete(murdername + ".pig");
            WriteSuccess(murdername + "已被成功杀死");
            Console.WriteLine("\n按任意键继续...");
            Console.ReadKey();
            goto Murder;
        Analysis:
            Console.Title = "Pig2019-连续分析器";
            Console.WriteLine("\n输入需要分析的猪名：");
            string anaName = Console.ReadLine(); if (anaName == "##") { goto Choose; }
            bool ex = ExistCheck(anaName);
            if (!ex)
            {
                Console.ReadKey(); Console.Clear(); goto Analysis;
            }
            Analysis(anaName);
            Console.WriteLine("\n按任意键继续...");
            Console.ReadKey();
            goto Analysis;
        }

        #region Proceed
        public static void Born(string name)                                               //Born
        {
            int[] g = new int[8128];
            Random ro = new Random();
            WriteInform("正在生成" + name + "的基因组");
            for (int i = 0; i <= 8127; i++)
            {
                g[i] = ro.Next(0, 10);
                Console.Write(g[i]);
            }
            WriteSuccess("----------完成\n");
            WriteInform("写入基因到" + name + ".pig...");
            string gs = string.Join(null, g.Select(p => p.ToString()).ToArray());
            File.WriteAllText(name, gs);
            File.Move(name, name + ".pig");
            WriteSuccess("\n----------完成----------\n");
        }
        public static void Cross(string a, string b, string c)                              //Cross
        {
            string g1 = File.ReadAllText(a + ".pig");
            string g2 = File.ReadAllText(b + ".pig");
            char[] ga = g1.ToCharArray();
            char[] gb = g2.ToCharArray();
            Random r1 = new Random(); char[] gNew = new char[8128];
            WriteInform("正在生成" + c + "的基因组");
            for (int i = 0; i <= 8127; i++)
            {
                int rnd = r1.Next(100);
                if (rnd <= 24 | (rnd <= 74 && rnd >= 50))
                {
                    gNew[i] = ga[i];
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(gNew[i]);
                }
                else if ((rnd >= 25 && rnd <= 50) | (rnd >= 75 && rnd <= 99))
                {
                    gNew[i] = gb[i];
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(gNew[i]);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            WriteSuccess("----------完成\n");
            WriteInform("写入基因到" + c + ".pig...");
            string gs = new string(gNew);
            File.WriteAllText(c + ".pig", gs);
            WriteSuccess("\n----------完成----------\n");
        }
        public static void Analysis(string name)
        {
            char[] g = File.ReadAllText(name + ".pig").ToCharArray();
            int[] count = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i <= 8127; i++)
            {
                switch (g[i])
                {
                    case '0': count[0]++; WriteClass(g[i]); break;
                    case '1': count[1]++; WriteClass(g[i]); break;
                    case '2': count[2]++; WriteClass(g[i]); break;
                    case '3': count[3]++; WriteClass(g[i]); break;
                    case '4': count[4]++; WriteClass(g[i]); break;
                    case '5': count[5]++; WriteClass(g[i]); break;
                    case '6': count[6]++; WriteClass(g[i]); break;
                    case '7': count[7]++; WriteClass(g[i]); break;
                    case '8': count[8]++; WriteClass(g[i]); break;
                    case '9': count[9]++; WriteClass(g[i]); break;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n" + name + "的基因统计：");
            for (int i = 0; i <= 9; i++)
            {
                Console.WriteLine("基因" + i + "的个数：" + count[i] + "  |  占比：" + Math.Round(Convert.ToSingle(count[i]) / 81.28, 2) + "%");
            }
        }
        public static void Feed(string name)
        {
            byte[] gs = new byte[3141592];
            for (long i = 0; i <= 3141591; i++)
            {
                gs[i] = 6;
            }
            FileStream fileStream = File.OpenWrite(name + ".pig");
            Console.WriteLine("关闭窗口以停止...");
            for (int i = 1; i >= 1; i++)
            {
                //Thread.Sleep(125);
                fileStream.Position = fileStream.Length;
                fileStream.Write(gs, 0, gs.Length);
            }
        }
        #endregion
        #region Functions
        public static bool ExistCheck(string name)
        {
            if (File.Exists(name + ".pig"))
            {
                WriteSuccess("成功读取" + name + ".pig");
                return true;
            }
            else
            {
                WriteError("\n" + name + "不存在"); Console.WriteLine("\n按任意键重输...");
                return false;
            }
        }
        public static string Size(long length)
        {
            string re;
            if (length < 1024)
            {
                re = length.ToString() + "B";
                return re;
            }
            else if (length > 1024 && length <= 1048576)
            {
                re = (length / 1024.0).ToString() + "kB";
                return re;
            }
            else if (length > 1048576 && length <= 1073741824)
            {
                re = (length / 1048576).ToString() + "MB";
                return re;
            }
            else
            {
                re = (length / 1073741824).ToString() + "GB";
                return re;
            }
        }
        #endregion
        #region Style
        public static void WriteError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteSuccess(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteInform(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteClass(char chr)
        {

            switch (chr)
            {
                case '0': Console.ForegroundColor = ConsoleColor.Red; Console.Write(chr); break;
                case '1': Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(chr); break;
                case '2': Console.ForegroundColor = ConsoleColor.Green; Console.Write(chr); break;
                case '3': Console.ForegroundColor = ConsoleColor.Blue; Console.Write(chr); break;
                case '4': Console.ForegroundColor = ConsoleColor.Magenta; Console.Write(chr); break;
                case '5': Console.ForegroundColor = ConsoleColor.Gray; Console.Write(chr); break;
                case '6': Console.ForegroundColor = ConsoleColor.DarkRed; Console.Write(chr); break;
                case '7': Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(chr); break;
                case '8': Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(chr); break;
                case '9': Console.ForegroundColor = ConsoleColor.White; Console.Write(chr); break;
            }

        }
        #endregion
    }
}
