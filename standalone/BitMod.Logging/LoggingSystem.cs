using System.IO;

using BitMod.Public;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

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
			.MinimumLevel.Verbose()
			.Enrich.WithThreadId()
			.Enrich.WithThreadName()
			.WriteTo.Console(
				LevelAlias.Minimum,
				theme: AnsiConsoleTheme.Sixteen,
				outputTemplate: "[{Timestamp:HH:mm:ss}] {Properties} {Level:u4}: {Message:lj}{NewLine}{Exception}" )
			.WriteTo.File(Path.Join(System.Environment.CurrentDirectory, LOG_PATH, LOG_NAME), LogEventLevel.Debug, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 60)
			.CreateLogger();
	}
}
