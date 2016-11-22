using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
#if __RCC_COMMENT__
using System.Windows.Forms;
#endif

using System.IO;

namespace SQLServerDB
{
    public enum LogLevel
    {
        Always = 0,
        Normal = 3,
        Development = 5
    }

    public class LogManager
    {
        public static void writeLog(Exception ex)
        {
            writeLog(((("\r\nErrorMessage: " + ex.Message) + "\r\nError Source: " + ex.Source) + "\r\nError Stack Trace: " + ex.StackTrace) + "\r\nError Target Site: " + ex.TargetSite, LogLevel.Always);
        }


        public static void writeLog(string log)
        {
            writeLog(log, LogLevel.Development);
        }

        public static void writeLog(string log, params object[] arg)
        {
            for (int i = 0; i < arg.Length; i++) {
                log = log.Replace("{" + i.ToString() + "}", arg[i].ToString());
            }
            writeLog(log, LogLevel.Development);
        }

        public static void writeLog(string log, LogLevel logLevel)
        {
            try {
                string fileName = ConfigurationManager.AppSettings["LOGFILE"];
                string path = ConfigurationManager.AppSettings["LOGDIRECTORY"];
                string fullpath = ConfigurationManager.AppSettings["LOGFULL"];

                if (fileName.Length > 0) {
                    StreamWriter writer;
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    FileInfo info = new FileInfo(path + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_" + fileName);

                    if (info.Exists) writer = info.AppendText();
                    else writer = info.CreateText();

                    writer.WriteLine(log);
                    writer.Close();
                } // if
            } catch (Exception ex) {
                String strLogMessage = "Exception[" + ex.Message + "]";
                LogManager.writeToLog(strLogMessage);
            }
        }


        public static void writeToLog(string log)
        {
            writeToLog(log, LogLevel.Development);
        }

        public static void writeToLog(string log, LogLevel logLevel)
        {
            try {
                string fileName = ConfigurationManager.AppSettings["LOGFILE"];
                string path = ConfigurationManager.AppSettings["LOGDIRECTORY"];
                string fullpath = ConfigurationManager.AppSettings["LOGFULL"];

                if (fileName.Length > 0) {
                    StreamWriter writer;
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    FileInfo info = new FileInfo(path + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_" + fileName);

                    if (info.Exists) writer = info.AppendText();
                    else writer = info.CreateText();

                    writer.WriteLine(log);
                    writer.Close();
                } // if
            } catch (Exception ex) {
                String strLogMessage = "Exception[" + ex.Message + "]";
                LogManager.writeToLog(strLogMessage);
            }
        }


        public static void writeQueryLog(string log)
        {
            writeQueryLog(log, LogLevel.Development);
        }

        public static void writeQueryLog(string log, LogLevel logLevel)
        {
            try {
                string fileName = ConfigurationManager.AppSettings["LOGFILE"];
                string path = ConfigurationManager.AppSettings["LOGDIRECTORY"];
                string fullpath = ConfigurationManager.AppSettings["LOGFULL"];

                if (fileName.Length > 0) {
                    StreamWriter writer;
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    FileInfo info = new FileInfo(path + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_" + fileName);

                    if (info.Exists) writer = info.AppendText();
                    else writer = info.CreateText();

                    writer.WriteLine(log);
                    writer.Close();
                } // if
            } catch (Exception ex) {
                String strLogMessage = "Exception[" + ex.Message + "]";
                LogManager.writeToLog(strLogMessage);
            }
        }

    }
}
