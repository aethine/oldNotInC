using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Not_In_C 
{
    static class Interpreter
    {
        static bool isEmpty(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
        static string[] param = null; //words for the line
        static bool Test(int place)
        {
            if (param.Length - 1 >= place) return true;
            else return false;
        } //tests if a parameter is in the place
        static int linenum = 1; //line number
        static bool TestParam(int place, string word, string errormessage = null)
        {
            if (Test(place) && (word == null || param[place] == word)) return true;
            else if (errormessage != null) Error(errormessage, true);
            return false;
        } //tests if a certain word in in the place
        static string[] getAllAfter(int place)
        {
            List<string> tor = new List<string>();
            if (Test(place))
            {
                for (int x = place + 1; Test(x); x++)
                    tor.Add(param[x]);
                return tor.ToArray();
            }
            else Error("Expected at least one word after word #" + (place), true);
            return null;
        } //getting all paramters after place
        static string expand(string name)
        {
            string t = name;
            if (name.StartsWith("@")) t = t.Split('@')[1];
            Memory.type g = Memory.VGet(t);
            if (g == Memory.type.Int)
                return Memory.IGet(t).ToString();
            else if (g == Memory.type.Bool)
                return Memory.BGet(t).ToString();
            else if (g == Memory.type.String)
                return Memory.SGet(t);
            else if (g == Memory.type.Double)
                return Memory.DGet(t).ToString();
            throw new Exception();
        } //variable expansion
        public static void Error(string message, bool showline = false)
        {
            Console.Write("ERROR: {0}", message);
            if (showline) Console.Write(" on line {0}", linenum);
            Console.WriteLine();
            Console.ReadKey();
            Environment.Exit(1);
        } //display message then quit
        public static void Run(string readpath)
        {
            Console.Title += ": " + readpath;
            Console.Clear();
            string[] lines = null;
            try { lines = System.IO.File.ReadAllLines(readpath); }
            catch (Exception e)
            {
                if (e is System.IO.FileNotFoundException || e is System.IO.DirectoryNotFoundException)
                    Error("File not found: \"" + readpath + "\"");
            }
            foreach (string s in lines) { Interpret(s); linenum++; } //} catch (NullReferenceException) { Error("File \"" + readpath +"\" invaid or empty"); }
        } //goes through all lines of readpath
        static void Interpret(string line) //interpreting a line
        {
            if (line.isEmpty() || line.StartsWith(";")) return;
            param = line.Trim().Split(' '); //removing excess whitespace then each element is 1 word
            if (TestParam(0, "pause")) { Console.ReadKey(true); }
            else if (TestParam(0, "write"))
            {
                string[] temp = getAllAfter(0);
                string top = null;
                for (int x = 0; temp.Length - 1 >= x; x++)
                {
                    if (temp[x].StartsWith("@"))
                        temp[x] = expand(temp[x]); //expanding variables
                    top += temp[x] + " ";
                }
                Console.Write(top.Remove(top.Length - 1));
            }
            else if (TestParam(0, "writeline"))
            {
                string top = null;
                if (Test(1))
                {
                    string[] temp = getAllAfter(0);
                    for (int x = 0; x < temp.Length; x++)
                    {
                        if (temp[x].StartsWith("@"))
                            temp[x] = expand(temp[x]); //expanding variables
                        top += temp[x] + " ";
                    }
                }
                Console.WriteLine(top.Remove(top.Length - 1));
            }
            else if (TestParam(0, "int"))
            {
                if (TestParam(1, null, "Invalid argument"))
                {
                    if (Test(2))
                        try { Memory.INew(param[1], int.Parse(param[2])); } catch (FormatException) { Error("Expected int value", true); }
                    else Memory.INew(param[1], 0); //default value
                }
            }
            else if (TestParam(0, "bool"))
            {
                if (TestParam(1, null, "Invalid argument"))
                {
                    if (Test(2))
                        try { Memory.BNew(param[1], bool.Parse(param[2])); } catch (FormatException) { Error("Expected boolean value", true); }
                    else Memory.BNew(param[1], false); //default value
                }
            }
            else if (TestParam(0, "string"))
            {
                if (TestParam(1, null, "Invalid argument"))
                {
                    if (Test(2))
                    {
                        string t = null;
                        foreach (string s in getAllAfter(1))
                            t += s + " ";
                        Memory.SNew(param[1], t.Remove(t.Length - 1));
                    }
                    else Memory.SNew(param[1], null); //default value
                }
            }
            else if (TestParam(0, "double"))
            {
                if (TestParam(1, null, "Invalid argument"))
                {
                    if (Test(2))
                        try { Memory.DNew(param[1], double.Parse(param[2])); } catch (FormatException) { Error("Expected double value", true); }
                    else Memory.DNew(param[1], 0); //default value
                }
            }

            else if (TestParam(0, "set"))
            {
                if (TestParam(1, null, "Invalid Argument"))
                {
                    string[] temp = getAllAfter(1);
                    string t = null;
                    for (int x = 0; x < temp.Length; x++)
                    {
                        if (temp[x].StartsWith("@"))
                            temp[x] = expand(temp[x]);
                        t += temp[x] + " ";
                    }//expanding variables
                    Memory.type g = new Memory.type();
                    g = Memory.VGet(param[1]);
                    DataTable d = new DataTable();
                    if (g == Memory.type.Int) Memory.ISet(param[1], int.Parse(d.Compute(t, "").ToString()));
                    else if (g == Memory.type.Bool) Memory.BSet(param[1], bool.Parse(t));
                    else if (g == Memory.type.String) Memory.SSet(param[1], t);
                    else if (g == Memory.type.Double) Memory.DSet(param[1], double.Parse(d.Compute(t, "").ToString()));
                }
            }
            else if (TestParam(0, "clear")) Console.Clear();
            else if (TestParam(0, "exit"))
            {
                if (Test(1))
                {
                    int o;
                    if (int.TryParse(param[1], out o)) Environment.Exit(o);
                    else Environment.Exit(0);
                }
                else Environment.Exit(0);
            }
            else { Error("Invalid keyword \"" + param[0] + "\"", true); }
        }
    }
}
