using BitMod.Configuration.Model;

namespace BitMod.Provision.Config;

public class ProvisionServerAdapter
{
	private const string MAPCYCLE = "mapcycle";
	private const string GAMEMODES = "gamemodes";
	private IConfigObject _configObject;

	public ProvisionServerAdapter(IConfigObject configObject)
	{
		_configObject = configObject;
	}

	public bool HasMapcycle()
		=> _configObject.Get(MAPCYCLE) != null;

	public bool HasGamemodes()
		=> _configObject.Get(GAMEMODES) != null;

	public string[]? GetMapcycle()
		=> _configObject.Get<IConfigObject>(MAPCYCLE)?.AsList()
			?.Select(model => model as IConfigSymbol)
			?.Where(symbol => symbol != null)
			?.Select(symbol => symbol.Symbol)
			?.ToArray();

	public string[]? GetGamemodes()
		=> _configObject.Get<IConfigObject>(GAMEMODES)?.AsList()
			?.Select(model => model as IConfigSymbol)
			?.Where(symbol => symbol != null)
			?.Select(symbol => symbol.Symbol)
			?.ToArray();


}
