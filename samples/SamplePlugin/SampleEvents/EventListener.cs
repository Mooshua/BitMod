using BitMod.Attributes.Injects;
using BitMod.Attributes.Mutators;
using BitMod.Attributes.Targets;
using BitMod.Configuration.Model;
using BitMod.Events.Meta;
using BitMod.Events.Server;
using BitMod.Flags.Attribute;

using Lilikoi.Standard;

using Serilog;

namespace SamplePlugin.SampleEvents;

public class EventListener
{

	[BitEvent]
	[Log]
	public async Task OnEvent(PluginLoadEvent ev, ILogger logger)
	{
		logger.Information("hello there, {@Name}!", ev.Name);
	}

	[BitEvent]
	[Log]
	public async Task OnServerConnect(GameServerConnectedEventArgs ev, [Flag("sample_flag", "nothing!")] IConfigSymbol flag, ILogger logger)
	{
		logger.Information("Hello new gameserver! Your flag value is {@Value}.", flag.Symbol);
	}
}
