using Logger.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Implementation
{
    public class Log : ILog
    {
        private static readonly object _object = new object();
        private static readonly object _objectex = new object();
        private readonly string path;
        public Log(string filepath)
        {
            path = filepath;
        }
        public void logMessage(string source, string request, string response = null)
        {
            Task.Run(() => logM(source, request, response));
        }

        public void logException(string source, Exception ex)
        {
            Task.Run(() => logE(source, ex));
        }
        private void logM(string source, string request, string response = null)
        {
            string filename = path + "\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\";

            if (!File.Exists(filename))
            {
                Directory.CreateDirectory(filename);
            }
            lock (_object)
            {
                using (TextWriter tw = new StreamWriter(filename  + DateTime.Now.ToString("yyyyMMdd") + "_Log.txt", true))
                {
                    tw.WriteLine("Date        : " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.TimeOfDay.ToString());
                    tw.WriteLine("SOURCE      : " + source);
                    if (!string.IsNullOrEmpty(request))
                    {
                        tw.WriteLine("REQUEST     : " + request);
                    }
                    if (!string.IsNullOrEmpty(response))
                    {
                        tw.WriteLine("RESPONSE    : " + response);
                    }
                    tw.WriteLine();
                    tw.Close();
                }
            }
        }

        private void logE(string source, Exception exception)
        {
            string filename = path + "\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\";

            if (!File.Exists(filename))
            {
                Directory.CreateDirectory(filename);
            }
            lock (_objectex)
            {
                using (TextWriter tw = new StreamWriter(filename + DateTime.Now.ToString("yyyyMMdd") + "_Exception.txt", true))
                {
                    tw.WriteLine("Date      :      " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.TimeOfDay.ToString());
                    tw.WriteLine("SOURCE    :      " + source);
                    tw.WriteLine("EXCEPTION :      " + exception.Message);
                    tw.WriteLine("INNEREXCEPTION : " + exception.InnerException);
                    tw.WriteLine("STACKTRACE : " + exception.StackTrace);
                    tw.WriteLine();
                    tw.Close();
                }
            }
        }
    }
}
