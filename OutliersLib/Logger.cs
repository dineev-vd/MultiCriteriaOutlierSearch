using System;
using System.IO;

namespace OutliersLib
{
    public static class Logger
    {
        //public static StreamWriter fs = File.AppendText("Log.txt");
        public static void Push(string message)
        {
            /*
            fs.WriteLine(DateTime.Now.ToString("hh.mm.ss.ffffff") + ": >" + message);
            fs.Flush();
            */
        }
    }
}