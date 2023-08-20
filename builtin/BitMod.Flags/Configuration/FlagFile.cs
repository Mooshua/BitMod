using BitMod.Configuration.Model;
using BitMod.Public;

using Serilog;

namespace BitMod.Flags.Configuration;

public class FlagFile
{

	public const string NAME = "flags";

	private readonly IConfigurationSystem _configurationSystem;
	private readonly IConfigObject _configObject;
	private readonly ILogger _logger;

	private readonly Dictionary<string, FlagGroup> _groups = new();
	private readonly FlagGroup _default;

	public FlagFile(IConfigObject configObject, ILogger logger, IConfigurationSystem configurationSystem)
	{
		_configObject = configObject;
		_logger = logger;
		_configurationSystem = configurationSystem;
		_default = Get("#default");
	}

	private IEnumerable<string> GetInherits(IConfigObject configObject)
		=> configObject.Get<IConfigObject>("Inherit")
			   ?.AsList()
			   ?.Select(entry => entry as IConfigSymbol)
			   ?.Where(entry => entry != null)
			   ?.Select(entry => entry.Symbol)
		   ?? Enumerable.Empty<string>();

	private IEnumerable<KeyValuePair<IConfigSymbol, IConfigSymbol>> GetSets(IConfigObject configObject)
		=> configObject.Get<IConfigObject>("Set")
			   ?.Select(kv => new KeyValuePair<IConfigSymbol, IConfigSymbol>(kv.Key, kv.Value as IConfigSymbol))
			   ?.Where(kv => kv.Value != null)
		   ?? Enumerable.Empty<KeyValuePair<IConfigSymbol, IConfigSymbol>>();

	public FlagGroup Get(string key = "#default")
	{
		if (_groups.TryGetValue(key, out var cached))
			return cached;

		List<string> seen = new();
		FlagGroup group = new(_configurationSystem);

		//	Inherit from default
		if (_default != null)
			foreach (var (s, value) in _default)
				group[s] = value;

		Dive(key, seen, group);

		return group;
	}

	private void Dive(string key, List<string> seen, FlagGroup group)
	{
		var obj = _configObject.Get<IConfigObject>(key);
		if (obj == null)
			return;

		seen.Add(key);

		foreach (string inherit in GetInherits(obj))
		{
			if (seen.Contains(inherit))
				continue;
			Dive(inherit, seen, group);
		}

		foreach (var (configSymbol, value) in GetSets(obj))
			group[configSymbol.Symbol] = value;
	}
}
