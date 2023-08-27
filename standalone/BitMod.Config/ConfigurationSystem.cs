using BitMod.Config.Cache;
using BitMod.Config.Implementation;
using BitMod.Configuration.Model;
using BitMod.Public;

using Lilikoi.Context;

using Serilog;

using ValveKeyValue;

namespace BitMod.Config;

public class ConfigurationSystem : IConfigurationSystem
{
	public static KVSerializer Serializer => KVSerializer.Create(KVSerializationFormat.KeyValues1Text);

	public static KVSerializerOptions Options => KVSerializerOptions.DefaultOptions;

	private ILogger _logger;
	private BitMod _parent;

	private Mount _cache = new Mount();

	public void Start(BitMod env)
	{
		_parent = env;
		_logger = _parent.Logger;

		//	Set up the cache
		_parent.Store(new ConfigCache(Options, Serializer, _logger, _parent.Invoker));
	}

	public IConfigObject Get(string name)
		=> _parent.Get<ConfigCache>().GetObject(name);

	public IConfigSymbol Fake(string symbol)
		=> new ConfigSymbol(symbol);
}
