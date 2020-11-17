using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.Interface
{
    public interface ILog
    {
        public void logMessage(string source, string request, string response = null);

        public void logException(string source, Exception ex);
    }
}
