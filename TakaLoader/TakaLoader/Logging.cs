﻿using System;
using System.IO;

namespace TakaLoader
{
    internal sealed class Logging
    {
        private static readonly string Logfile = Path.Combine(Path.GetTempPath(), "TakaLoader.log");
        private static readonly object _lock = new object();

        public static bool Log(string text, bool ret = true)
        {
            var newline = "\n";
            if (text.Length > 40 && text.Contains("\n"))
                newline += "\n\n";
            Console.Write(text + newline);
            if (Config.DebugMode == "1")
            {
                lock (_lock)
                {
                    File.AppendAllText(Logfile, text + newline);
                }
            }
            return ret;
        }

        public static void Save(string sSavePath)
        {
            if (Config.DebugMode != "1" || !File.Exists(Logfile)) return;
            try
            {
                File.Copy(Logfile, sSavePath);
            }
            catch
            {
                // ignored
            }
        }
    }
}