using Serilog;

namespace BitMod.Config;

public class ConfigurationCache<T>
{
	private FileSystemWatcher _watcher;

	private string _file;
	private string _name;

	private Func<T> _makeDefault;
	private ILogger _logger;

	public ConfigurationCache(string file, string name, Func<T> makeDefault, ILogger logger)
	{
		_file = file;
		_name = name;
		_makeDefault = makeDefault;
		_logger = logger;

		_watcher = new FileSystemWatcher();
		_watcher.Path = Path.Join( System.Environment.CurrentDirectory, ConfigurationSystem.CONFIG_PATH );
		_watcher.Changed += OnChanged;
		_watcher.Filter = name;
		_watcher.NotifyFilter = NotifyFilters.LastWrite;
		_watcher.EnableRaisingEvents = true;

		Value = ConfigurationSystem.ReadFile(file, name, makeDefault);
	}

	internal void OnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
	{
		try
		{
			Value = ConfigurationSystem.ReadFile(_file, _name, _makeDefault);
		}
		catch (Exception e)
		{
			_logger.Warning(e, "[BitMod Config] Failed to get new value of {@File}", _file);
		}
	}

	public T Value { get; set; }
}
