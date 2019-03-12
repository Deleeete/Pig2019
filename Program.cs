using System;
using System.IO;
using Libs;

namespace 猪圈2019
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
                Console.WriteLine("1.生猪\n2.喂猪\n3.杂交\n4.杀猪\n5.分析\n6.快速杂交+分析");
                string str = Console.ReadLine(); byte mode; Console.Clear();
                try { mode = Convert.ToByte(str); }
                catch { Style.WriteError("\n非法输入，按任意键重输\n"); Console.ReadKey(); goto Choose; }
                if (mode == 1) { goto Born; }
                else if (mode == 2) { goto Feed; }
                else if (mode == 3) { goto Cross; }
                else if (mode == 4) { goto Murder; }
                else if (mode == 5) { goto Analysis; }
                else if (mode == 6) { P.Flow(); }
                else { Style.WriteError("没有找到对应的选项，按任意键重输\n"); Console.ReadKey(); goto Choose; }
            }
            else if (args[0] == "1") { goto Born; }
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
                Style.WriteError("同名猪已存在"); Console.WriteLine("按任意键重新输入"); Console.ReadKey(); goto Born;
            }
            P.Born(bornname);
            P.Analysis(bornname, 1);
            Console.WriteLine("其基因组是否合适？(Y/n)");
            string i = Console.ReadLine();
            if (i == "y" | i == "Y")
            {
                goto Born;
            }
            else if (i == "n" | i == "N")
            {
                File.Delete(bornname + ".pig");
                Style.WriteSuccess("成功杀死了不合格的种猪" + bornname); Console.WriteLine("\n按任意键继续...");
                Console.ReadKey();
                goto Born;
            }
        Feed:
            Console.Clear();
            Console.Title = "Pig2019-喂猪器";
            Console.WriteLine("喂猪模式\n*****************");
            Console.WriteLine("\n输入需要喂的猪名：");
            string feedname = Console.ReadLine(); if (feedname == "##") { goto Choose; }
            bool ed = Function.ExistCheck(feedname);
            if (!ed) { Console.ReadKey(); goto Feed; }
            Style.WriteError("注意：喂猪会导致猪的体积快速增长，速度可接近硬盘的最大写入速度，是否真的要继续？（Y/n）");
            string choice = Console.ReadLine();
            if (choice == "Y" | choice == "y")
            {
                P.Feed(feedname);
            }
            else { goto Choose; }
        Cross:
            Console.Clear();
            Console.Title = "Pig2019-杂交器";
            Console.WriteLine("连续杂交模式\n*****************");
            Console.WriteLine("\n输入两只亲本猪和子代猪的名字，分别使用空格分隔：\n(亲代1 亲代2 子代)");
            string parents = Console.ReadLine();
            if (parents == "##") { goto Choose; }
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
            catch { Style.WriteError("\n发生错误，检查分隔符是否正确。"); Console.WriteLine("按任意键重新输入..."); Console.ReadKey(); goto Cross; }
            P.Cross(prt[0], prt[1], prt[2]);
            Console.WriteLine("\n按任意键继续...");
            Console.ReadKey();
            goto Cross;

        Murder:
            Console.Clear();
            Console.Title = "Pig2019-杀猪器";
            Console.WriteLine("杀猪模式\n*****************");
            Console.WriteLine("\n输入想要谋杀的猪的名字：");
            string murdername = Console.ReadLine(); if (murdername == "##") { goto Choose; }
            bool ea = Function.ExistCheck(murdername);
            if (!ea)
            { Console.ReadKey(); goto Murder; }
            File.Delete(murdername + ".pig");
            Style.WriteSuccess(murdername + "已被成功杀死");
            Console.WriteLine("\n按任意键继续...");
            Console.ReadKey();
            goto Murder;
        Analysis:
            Console.Title = "Pig2019-连续分析器";
            Console.WriteLine("\n输入需要分析的猪名：");
            string anaName = Console.ReadLine(); if (anaName == "##") { goto Choose; }
            bool ex = Function.ExistCheck(anaName);
            if (!ex)
            {
                Console.ReadKey(); Console.Clear(); goto Analysis;
            }
            P.Analysis(anaName);
            Console.WriteLine("\n按任意键继续...");
            Console.ReadKey();
            goto Analysis;
        }
    }
}
