using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Logger
{
    public class LoggerService<T> : ILoggerService<T>
    {
        public readonly ILog _log;
        

        public LoggerService()
        {
            string log4NetConfigDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            string log4NetConfigFilePath = Path.Combine(log4NetConfigDirectory, "log4net.config");
            XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigFilePath));
            _log = LogManager.GetLogger(typeof(LoggerService<T>));
        }


        public void WriteError(string s)
        {
            _log.Error(s);
        }

        public void WriteInfo(string s)
        {
            _log.Info(s);

        }

        public void WriteWarn(string s)
        {
            _log.Warn(s);
        }

        public void WriteFatal(string s)
        {
            _log.Fatal(s);
        }

    }
}
