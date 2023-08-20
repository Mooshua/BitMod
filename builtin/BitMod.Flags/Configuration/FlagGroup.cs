using BitMod.Configuration.Model;
using BitMod.Public;

namespace BitMod.Flags.Configuration;

public class FlagGroup : Dictionary<string, IConfigSymbol>
{
	private IConfigurationSystem _configurationSystem;

	public FlagGroup(IConfigurationSystem configurationSystem)
	{
		_configurationSystem = configurationSystem;
	}

	public IConfigSymbol Fetch(string name, string fallback)
	{
		if (TryGetValue(name, out var value))
			return value;

		return _configurationSystem.Fake(fallback);
	}
}
