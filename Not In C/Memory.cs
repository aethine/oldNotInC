using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Not_In_C
{
    static class Memory
    {
        public enum type { Int, Bool, String, Double} //for easy communication between functions
        public static Nint[] Int = new Nint[1024]; //memory allocated for integers
        public static Nbool[] Bool = new Nbool[1024]; //memory allocated for booleans
        public static Nstring[] String = new Nstring[1024]; //memory allocated for strings
        public static Ndouble[] Double = new Ndouble[1024]; //memory allocated for doubles
        /* the first letter indicates the type (I = int, B = bool, S = string, D = Double, V = generic type)
         * new adds a new object to its allocated memory
         * get gets the value of an object based on the name
         * set sets the value of an object based on the name
         */
        public static type VGet(string name)
        {
            foreach (Nint n in Int)
            {
                if (n == null) break;
                if (n.name == name) return type.Int;
            }
            foreach (Nbool n in Bool)
            {
                if (n == null) break;
                if (n.name == name) return type.Bool;
            }
            foreach (Nstring n in String)
            {
                if (n == null) break;
                if (n.name == name) return type.String;
            }
            foreach (Ndouble n in Double)
            {
                if (n == null) break;
                if (n.name == name) return type.Double;
            }
            Interpreter.Error("Variable not found: \"" + name + "\"", true);
            throw new ArgumentException();
        }
        public static void VSet(string name, string value)
        {
            type t = VGet(name);
            if (t == type.Int) ISet(name, int.Parse(value));
            else if (t == type.Bool) BSet(name, bool.Parse(value));
            else if (t == type.String) BSet(name, bool.Parse(value));
            else if (t == type.Double) BSet(name, bool.Parse(value));
            Interpreter.Error("Variable not found: \"" + name + "\"", true);
            throw new ArgumentException();
        }
        public static void INew(string name, int value)
        {
            int counter = 0;
            for(int n = 0; n <= 1024; n++)
            {
                if (Int[n] == null) { Int[counter] = new Nint(name, value); return; }
                else if (Int[n].name == name) Interpreter.Error("Int not found: \"" + name + "\"", true);
                counter++;
            }
        }
        public static int IGet(string name)
        {
            for(int x = 0; x <= 1024; x++)
            {
                if (Int[x] == null) break;
                if (Int[x].name == name) return Int[x].value;
            }
            Interpreter.Error("Int not found: \"" + name + "\"", true);
            throw new ArgumentException();
        }
        public static void ISet(string name, int value)
        {
            for(int x = 0; x <= 1024; x++)
            {
                if (Int[x] == null) break;
                if(Int[x].name == name) { Int[x].value = value; return; }
            }
            Interpreter.Error("Int not found: \"" + name + "\"", true);
            throw new ArgumentException();
        }
        public static void BNew(string name, bool value)
        {
            int counter = 0;
            for (int n = 0; n <= 1024; n++)
            {
                if (Bool[n] == null) { Bool[counter] = new Nbool(name, value); return; }
                else if (Bool[n].name == name) Interpreter.Error("Boolean not found: \"" + name + "\"", true);
                counter++;
            }
        }
        public static bool BGet(string name)
        {
            for (int x = 0; x <= 1024; x++)
            {
                if (Bool[x] == null) break;
                if (Bool[x].name == name) return Bool[x].value;
            }
            Interpreter.Error("Boolean not found: \"" + name + "\"", true);
            throw new ArgumentException();
        }
        public static void BSet(string name, bool value)
        {
            for (int x = 0; x <= 1024; x++)
            {
                if (Bool[x] == null) break;
                if (Bool[x].name == name) { Bool[x].value = value; return; }
            }
            Interpreter.Error("Boolean not found: \"" + name + "\"", true);
            throw new ArgumentException();
        }
        public static void SNew(string name, string value)
        {
            int counter = 0;
            for (int n = 0; n <= 1024; n++)
            {
                if (String[n] == null) { String[counter] = new Nstring(name, value); return; }
                else if (String[n].name == name) Interpreter.Error("String not found: \"" + name + "\"", true);
                counter++;
            }
        }
        public static string SGet(string name)
        {
            for (int x = 0; x <= 1024; x++)
            {
                if (String[x] == null) break;
                if (String[x].name == name) return String[x].value;
            }
            Interpreter.Error("String not found: \"" + name + "\"", true);
            throw new ArgumentException();
        }
        public static void SSet(string name, string value)
        {
            for (int x = 0; x <= 1024; x++)
            {
                if (String[x] == null) break;
                if (String[x].name == name) { String[x].value = value; return; }
            }
            Interpreter.Error("String not found: \"" + name + "\"", true);
            throw new ArgumentException();
        }
        public static void DNew(string name, double value)
        {
            int counter = 0;
            for (int n = 0; n <= 1024; n++)
            {
                if (Double[n] == null) { Double[counter] = new Ndouble(name, value); return; }
                else if (Double[n].name == name) Interpreter.Error("Double not found: \"" + name + "\"", true);
                counter++;
            }
        }
        public static double DGet(string name)
        {
            for (int x = 0; x <= 1024; x++)
            {
                if (Double[x] == null) break;
                if (Double[x].name == name) return Double[x].value;
            }
            Interpreter.Error("Double not found: \"" + name + "\"", true);
            throw new ArgumentException();
        }
        public static void DSet(string name, double value)
        {
            for (int x = 0; x <= 1024; x++)
            {
                if (Double[x] == null) break;
                if (Double[x].name == name) { Double[x].value = value; return; }
            }
            Interpreter.Error("Double not found: \"" + name + "\"", true);
            throw new ArgumentException();
        }
    }
    class Nint
    {
        public string name;
        public int value;
        public Nint(string name, int value) { this.name = name; this.value = value; }
    }
    class Nbool
    {
        public string name;
        public bool value;
        public Nbool(string name, bool value) { this.name = name; this.value = value; }
    }
    class Nstring
    {
        public string name;
        public string value;
        public Nstring(string name, string value) { this.name = name; this.value = value; }
    }
    class Ndouble
    {
        public string name;
        public double value;
        public Ndouble(string name, double value) { this.name = name; this.value = value; }
    }
    //static class Equate
    //{
    //    static bool isBOperator(this string q)
    //    {
    //        switch (q)
    //        {
    //            case "==":
    //                return true;
    //            case "+":
    //                return true;
    //            case "*":
    //                return true;
    //            case "^":
    //                return true;
    //            default:
    //                return false;
    //        }
    //    }
    //    static bool isIOperator(this string q)
    //    {
    //        switch (q)
    //        {
    //            case "==": return true;
    //            case ">": return true;
    //            case "<": return true;
    //            case "!=": return true;
    //            case "<=": return true;
    //            case ">=": return true;
    //            default: return false;
    //        }
    //    }
    //    public static bool Boolean(string i) //equation elements must be split with spaces
    //    {
    //        bool by;
    //        int iy;
    //        string t0 = null, t1 = null, t2 = null;
    //        string[] temp = i.Split(' ');
    //        foreach (string t in temp)
    //            if (!(bool.TryParse(t, out by) || (isBOperator(t) || isIOperator(t)))) throw new ArithmeticException();
    //        for (int x = 1; x < temp.Length; x++)
    //        {
    //            t0 = temp[x - 1]; t1 = temp[x]; t2 = temp[x + 1];
    //            if (!bool.TryParse(t1, out by) || !int.TryParse(t1, out iy))
    //            {
    //                if (t1.isBOperator())
    //                {
    //                    if()
    //                }
    //            }
    //        }
    //    }
    //}
}
