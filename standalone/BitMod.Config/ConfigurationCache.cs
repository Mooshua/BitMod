namespace BitMod.Config;

public class ConfigurationCache<T>
{
	private FileSystemWatcher _watcher;

	private string _file;
	private string _name;

	private Func<T> _makeDefault;

	public ConfigurationCache(string file, string name, Func<T> makeDefault)
	{
		_file = file;
		_name = name;
		_makeDefault = makeDefault;
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
		Value = ConfigurationSystem.ReadFile(_file, _name, _makeDefault);
	}

	public T Value { get; set; }
}
