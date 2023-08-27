#region

using BitMod.Config.Implementation;
using BitMod.Configuration.Model;
using BitMod.Internal.Public;

using Serilog;

using ValveKeyValue;

#endregion

namespace BitMod.Config.Cache;

public class ConfigWatcher
{
	private static string CONFIG_PATH = "configs";
	private static string CONFIG_EXT = "cfg";

	private string _configFileName;

	private string _configFilePath;

	private IConfigObject _configObject;

	private ILogger _logger;

	private PluginInvoker _invoker;

	private KVSerializer _serializer;

	private KVSerializerOptions _serializerOptions;

	private FileSystemWatcher _watcher;

	public ConfigWatcher(string configFileName, ILogger logger, KVSerializer serializer, KVSerializerOptions serializerOptions, PluginInvoker invoker)
	{
		_configFileName = configFileName;
		_configFilePath = Path.Join(System.Environment.CurrentDirectory, CONFIG_PATH, $"{_configFileName}.{CONFIG_EXT}");
		_logger = logger;
		_serializer = serializer;
		_serializerOptions = serializerOptions;
		_invoker = invoker;
		//	Create an empty config object
		_configObject = new ConfigObject(
			new KVObject(_configFileName, Enumerable.Empty<KVObject>()));

		_watcher = new FileSystemWatcher();
		_watcher.Path = Path.Join(System.Environment.CurrentDirectory, CONFIG_PATH);
		_watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.Size;
		_watcher.Filter = $"{configFileName}.{CONFIG_EXT}";
		_watcher.Created += OnChange;
		_watcher.Changed += OnChange;
		_watcher.EnableRaisingEvents = true;

		TryRead();
	}

	public IConfigObject Object => _configObject;

	private void OnChange(object sender, FileSystemEventArgs args)
	{
		_logger.Information("[BitMod Config] Reloading config file {@Name} due to update.", _configFileName);
		TryRead();

		_invoker.ConfigUpdate(_configFileName, _configObject);
	}


	private void TryRead()
	{
		try
		{
			using (var reader = File.OpenRead(_configFilePath))
			{
				var kvObject = _serializer.Deserialize(reader, _serializerOptions);
				_configObject = new ConfigObject(kvObject);
			}
		}
		catch (Exception ex)
		{
			_logger.Warning(ex, "[BitMod Config] Failed to reload config file {@Name} due to error while parsing.", _configFileName);
		}
	}
}
