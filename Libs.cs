using System;
using System.IO;
using System.Linq;

namespace Libs
{
    class P
    {
        public static void Born(string name)                                               //Born
        {
            int[] g = new int[8128];
            Style.WriteInform("正在生成" + name + "的基因组");
            Random ro = new Random(Function.GetRandomSeed());
            for (int i = 0; i <= 8127; i++)
            {
                g[i] = ro.Next(0, 10);
                Console.Write(g[i]);
            }
            Style.WriteSuccess("----------完成\n");
            Style.WriteInform("写入基因到" + name + ".pig...");
            string gs = string.Join(null, g.Select(p => p.ToString()).ToArray());
            File.WriteAllText(name, gs);
            File.Move(name, name + ".pig");
            Style.WriteSuccess("\n----------完成----------\n");
        }
        public static void Cross(string a, string b, string c)                              //Cross
        {
            string g1 = File.ReadAllText(a + ".pig");
            string g2 = File.ReadAllText(b + ".pig");
            char[] ga = g1.ToCharArray();
            char[] gb = g2.ToCharArray();
            char[] gNew = new char[8128];
            Style.WriteInform("正在生成" + c + "的基因组");
            Random r1 = new Random(Function.GetRandomSeed());
            for (int i = 0; i <= 8127; i++)
            {
                int rnd = r1.Next(100);int rndi = r1.Next(8128);
                if (rnd % 2 == 0)
                { gNew[i] = ga[rndi];Console.ForegroundColor = ConsoleColor.Blue;Console.Write(gNew[i]); }
                else
                { gNew[i] = gb[rndi]; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write(gNew[i]); }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Style.WriteSuccess("--完成--------");
            Style.WriteInform("写入基因到" + c + ".pig...");
            string gs = new string(gNew);
            File.WriteAllText(c + ".pig", gs);
            Style.WriteSuccess("--完成--------");
        }
        public static void Analysis(string name)
        {
            char[] g = File.ReadAllText(name + ".pig").ToCharArray();
            int[] count = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i <= 8127; i++)
            {
                switch (g[i])
                {
                    case '0': count[0]++; Style.WriteClass(g[i]); break;
                    case '1': count[1]++; Style.WriteClass(g[i]); break;
                    case '2': count[2]++; Style.WriteClass(g[i]); break;
                    case '3': count[3]++; Style.WriteClass(g[i]); break;
                    case '4': count[4]++; Style.WriteClass(g[i]); break;
                    case '5': count[5]++; Style.WriteClass(g[i]); break;
                    case '6': count[6]++; Style.WriteClass(g[i]); break;
                    case '7': count[7]++; Style.WriteClass(g[i]); break;
                    case '8': count[8]++; Style.WriteClass(g[i]); break;
                    case '9': count[9]++; Style.WriteClass(g[i]); break;
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

        public static void Flow()
        {
        Cross:
            Console.Title = "FCaSAS";
            Console.WriteLine("\n输入两只亲本猪和子代猪的名字，分别使用空格分隔：\n(亲代1 亲代2 子代)");
            string parents = Console.ReadLine();
            if (parents == "##") { goto Cross; }
            string[] prt;
            try
            {
                prt = parents.Split(' ');
                Style.WriteInform("\n亲本1：" + prt[0] + "\n亲本2：" + prt[1] + "\n子代：" + prt[2]);
                bool eb = Function.ExistCheck(prt[0]);
                if (!eb) { Console.ReadKey(); Console.Clear(); goto Cross; }
                bool ec = Function.ExistCheck(prt[1]);
                if (!ec) { Console.ReadKey(); Console.Clear(); goto Cross; }
                if (File.Exists(prt[2] + ".pig")) { Style.WriteError("子代" + prt[2] + "已经存在"); Console.WriteLine("按任意键重新输入..."); Console.ReadKey(); goto Cross; }
            }
            catch { Style.WriteError("\n发生错误，检查分隔符是否正确。"); Console.WriteLine("按任意键重新输入..."); Console.ReadKey(); Console.Clear(); goto Cross; }
            P.Cross(prt[0], prt[1], prt[2], 1);
            P.Analysis(prt[2], 1);
            goto Cross;
        }

        #region Overload
        public static void Cross(string a, string b, string c, byte sign)                              //Cross
        {
            string g1 = File.ReadAllText(a + ".pig");
            string g2 = File.ReadAllText(b + ".pig");
            char[] ga = g1.ToCharArray();
            char[] gb = g2.ToCharArray();
            char[] gNew = new char[8128];
            Style.WriteInform("正在生成" + c + "的基因组");
            Random r1 = new Random(Function.GetRandomSeed());
            for (int i = 0; i <= 8127; i++)
            {
                int rnd = r1.Next(100); int rndi = r1.Next(8128);
                if (rnd <= 24 | (rnd <= 74 && rnd >= 50))
                {
                    gNew[i] = ga[rndi];
                }
                else if ((rnd >= 25 && rnd <= 50) | (rnd >= 75 && rnd <= 99))
                {
                    gNew[i] = ga[rndi];
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Style.WriteSuccess("--完成--------");
            Style.WriteInform("写入基因到" + c + ".pig...");
            string gs = new string(gNew);
            File.WriteAllText(c + ".pig", gs);
            Style.WriteSuccess("--完成--------");
        }
        public static void Analysis(string name, byte sign)
        {
            char[] g = File.ReadAllText(name + ".pig").ToCharArray();
            int[] count = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i <= 8127; i++)
            {
                switch (g[i])
                {
                    case '0': count[0]++; break;
                    case '1': count[1]++; break;
                    case '2': count[2]++; break;
                    case '3': count[3]++; break;
                    case '4': count[4]++; break;
                    case '5': count[5]++; break;
                    case '6': count[6]++; break;
                    case '7': count[7]++; break;
                    case '8': count[8]++; break;
                    case '9': count[9]++; break;
                }
            }
            Console.WriteLine("\n" + name + "的基因统计：");
            for (int i = 0; i <= 9; i++)
            {
                Console.WriteLine("基因" + i + "的个数：" + count[i] + "  |  占比：" + Math.Round(Convert.ToSingle(count[i]) / 81.28, 2) + "%");
            }
        }
        #endregion
    }
    class Style
    {
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
    }
    class Function
    {
        public static bool ExistCheck(string name)
        {
            if (File.Exists(name + ".pig"))
            {
                Style.WriteSuccess("成功读取" + name + ".pig");
                return true;
            }
            else
            {
                Style.WriteError("\n" + name + "不存在"); Console.WriteLine("\n按任意键重输...");
                return false;
            }
        }
        public static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
