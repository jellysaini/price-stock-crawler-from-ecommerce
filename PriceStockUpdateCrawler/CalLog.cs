using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace PriceStockUpdateCrawler
{
    #region Add Logs
    public static class CalLog
    {
        public static void AddtoLogFile(string Message, string ErrorSoruce)
        {
            string LogPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\logs\\";
            string filename = "Log_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            string filepath = LogPath + filename;

            if (File.Exists(filepath))
            {
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    writer.WriteLine("-------------------START-------------" + DateTime.Now);
                    writer.WriteLine("Source :" + ErrorSoruce);
                    writer.WriteLine(Message);
                    writer.WriteLine("-------------------END-------------" + DateTime.Now);
                }
            }
            else
            {
                StreamWriter writer = File.CreateText(filepath);
                writer.WriteLine("-------------------START-------------" + DateTime.Now);
                writer.WriteLine("Source :" + ErrorSoruce);
                writer.WriteLine(Message);
                writer.WriteLine("-------------------END-------------" + DateTime.Now);
                writer.Close();
            }
        }
    }

    #endregion


}