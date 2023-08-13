using BitMod.Public;

using Lilikoi.Context;

using Serilog;

using ValveKeyValue;

namespace BitMod.Config;

public class ConfigurationSystem : IConfigurationSystem
{
	public static string CONFIG_PATH = "configs";
	public static string CONFIG_EXT = ".kv";

	public static KVSerializer Serializer => KVSerializer.Create(KVSerializationFormat.KeyValues1Text);

	public static KVSerializerOptions Options => KVSerializerOptions.DefaultOptions;

	private ILogger _logger;
	private BitMod _parent;

	private Mount _cache = new Mount();

	public void Start(BitMod env)
	{
		_parent = env;
		_logger = _parent.Logger;
	}

	private void StartFileWatcher()
	{

	}

	public static T ReadFile<T>(string path, string name, Func<T> makeDefault)
	{
		if (!File.Exists(path))
		{
			var def = makeDefault();

			using (var stream = File.OpenWrite(path))
				Serializer.Serialize(stream, def, name, Options);
		}

		using (var stream = File.OpenRead(path))
		{
			var model = Serializer.Deserialize<T>(stream, Options);
			return model;
		}
	}

	public T Get<T>(string name, Func<T> makeDefault)
	{
		try
		{
			if (!_cache.Has<ConfigurationCache<T>>())
			{
				var path = Path.Join(System.Environment.CurrentDirectory, CONFIG_PATH, name + CONFIG_EXT);
				_cache.Store(new ConfigurationCache<T>(path, name, makeDefault, _logger));
			}

			return _cache.Get<ConfigurationCache<T>>()!.Value;
		}
		catch (Exception e)
		{
			_logger.Warning(e,"[BitMod Config] Failed to fetch config {@Name}. Using default", name);
		}
		return makeDefault();
	}
}
