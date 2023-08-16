using System.Security;

using BitMod.Configuration.Model;

using Serilog;

using ValveKeyValue;

namespace BitMod.Config.Cache;

public class ConfigCache
{

	private ILogger _logger;
	private KVSerializer _serializer;
	private KVSerializerOptions _serializerOptions;
	private Dictionary<string, ConfigWatcher> _watchers = new();

	public ConfigCache(KVSerializerOptions serializerOptions, KVSerializer serializer, ILogger logger)
	{
		_serializerOptions = serializerOptions;
		_serializer = serializer;
		_logger = logger;
	}

	public ConfigWatcher GetWatcher(string file)
	{
		if (_watchers.TryGetValue(file, out var watcher))
			return watcher;

		_watchers[file] = new ConfigWatcher(file, _logger, _serializer, _serializerOptions);
		return _watchers[file];
	}

	public IConfigObject GetObject(string file)
		=> GetWatcher(file).Object;

}
