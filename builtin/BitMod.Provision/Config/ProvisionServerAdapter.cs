using BitMod.Configuration.Model;

namespace BitMod.Provision.Config;

public class ProvisionServerAdapter
{
	private const string MAPCYCLE = "mapcycle";
	private const string GAMEMODES = "gamemodes";
	private const string LOADING_TEXT = "loading_text";
	private const string MIN_PLAYERS = "min_players";

	private IConfigObject _configObject;

	public ProvisionServerAdapter(IConfigObject configObject)
	{
		_configObject = configObject;
	}

	public bool HasMapcycle()
		=> _configObject.Get<IConfigObject>(MAPCYCLE) != null;

	public bool HasGamemodes()
		=> _configObject.Get<IConfigObject>(GAMEMODES) != null;

	public bool HasLoadingText()
		=> _configObject.Get<IConfigSymbol>(LOADING_TEXT) != null;

	public bool HasMinPlayers()
		=> _configObject.Get<IConfigSymbol>(MIN_PLAYERS) != null;

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

	public string? GetLoadingText()
		=> _configObject.Get<IConfigSymbol>(LOADING_TEXT)?.Symbol;

	public long? GetMinPlayers()
		=> _configObject.Get<IConfigSymbol>(MIN_PLAYERS)?.AsInt;


}
