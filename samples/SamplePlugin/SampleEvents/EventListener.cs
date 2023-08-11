using BitMod.Attributes.Targets;
using BitMod.Events.Meta;

using Lilikoi.Standard;

using Serilog;

namespace SamplePlugin.SampleEvents;

public class EventListener
{
	[Singleton]
	private ILogger _logger;

	[BitEvent]
	public Task OnEvent(PluginLoadEvent ev)
	{
		_logger.Information("hello there, {@Name}!", ev.Name);

		return Task.CompletedTask;
	}
}
