using BitMod.Public;

using Serilog;
using Serilog.Events;

namespace BitMod.Logging;

public class LoggingSystem
{
	private IConfigurationSystem _config;
	private LoggingConfiguration _loggingConfiguration;

	public LoggingSystem(IConfigurationSystem config)
	{
		_config = config;
		_loggingConfiguration = config.Get("logging", () => new LoggingConfiguration());
	}

	public static string LOG_PATH = "logs";
	public static string LOG_NAME = "log.txt";

	public ILogger GetLogger()
	{
		return new LoggerConfiguration()
			.WriteTo.Console( LogEventLevel.Verbose )
			.WriteTo.File(Path.Join(System.Environment.CurrentDirectory, LOG_PATH, LOG_NAME), LogEventLevel.Debug)
			.CreateLogger();
	}
}
