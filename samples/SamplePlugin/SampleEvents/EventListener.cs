using BitMod.Attributes.Injects;
using BitMod.Attributes.Mutators;
using BitMod.Attributes.Targets;
using BitMod.Events.Meta;

using Lilikoi.Standard;

using Serilog;

namespace SamplePlugin.SampleEvents;

public class EventListener
{
	[BitEvent]
	[Log]
	public Task OnEvent(PluginLoadEvent ev, ILogger logger)
	{
		logger.Information("hello there, {@Name}!", ev.Name);

		return Task.CompletedTask;
	}
}
