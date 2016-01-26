namespace Poi
{
    public interface ILogger
    {
		void Write(object obj);
		void Write(string format, params object[] args);
		void WriteLine(object obj);
		void WriteLine(string format, params object[] args);

		void Warn(object obj);
		void Warn(string format, params object[] args);
		void WarnLine(object obj);
		void WarnLine(string format, params object[] args);

		void Error(object obj);
		void Error(string format, params object[] args);
		void ErrorLine(object obj);
		void ErrorLine(string format, params object[] args);

		void Close();
    }
}