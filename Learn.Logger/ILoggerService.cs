using log4net.Core;

namespace Learn.Logger
{
    public interface ILoggerService<T>
    {
        void WriteError(string s);
        void WriteInfo(string s);
        void WriteWarn(string s);
        void WriteFatal(string s);
    }
}