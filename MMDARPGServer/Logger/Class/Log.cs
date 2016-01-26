namespace Poi
{
    /// <summary>
    /// 记录
    /// </summary>
	public static class Log
	{
		static private ILogger logger;

		static public void Write(object obj)
		{
			logger.Write(obj);
		}
		static public void Write(string format, params object[] args)
		{
			logger.Write(format, args);
		}
		static public void WriteLine(object obj)
		{
			logger.WriteLine(obj);
		}
		static public void WriteLine(string format, params object[] args)
		{
			logger.WriteLine(format, args);
		}

        static public void Warn(object obj)
		{
			logger.Warn(obj);
		}
		static public void Warn(string format, params object[] args)
		{
			logger.Warn(format, args);
		}
		static public void WarnLine(object obj)
		{
			logger.WarnLine(obj);
		}
		static public void WarnLine(string format, params object[] args)
		{
			logger.WarnLine(format, args);
		}

		static public void Error(object obj)
		{
			logger.Error(obj);
		}
		static public void Error(string format, params object[] args)
		{
			logger.Error(format, args);
		}
		static public void ErrorLine(object obj)
		{
			logger.ErrorLine(obj);
		}
		static public void ErrorLine(string format, params object[] args)
		{
			logger.ErrorLine(format, args);
		}

		static public void Close()
		{
			logger.Close();
		}

		static public void SetGlobalLogger(ILogger logger)
		{
			Log.logger = logger;
		}
	}
}