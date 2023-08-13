using BitMod.Public;

using Serilog;
using Serilog.Events;

namespace BitMod.Logging;

public class LoggingSystem
{
	private IConfigurationSystem _config;

	public LoggingSystem(IConfigurationSystem config)
	{
		_config = config;
	}

	public static string LOG_PATH = "logs";
	public static string LOG_NAME = "log.txt";

	public ILogger GetLogger()
	{
		return new LoggerConfiguration()
			.WriteTo.Console( LogEventLevel.Debug )
			.WriteTo.File(Path.Join(System.Environment.CurrentDirectory, LOG_PATH, LOG_NAME), LogEventLevel.Debug)
			.CreateLogger();
	}
}
