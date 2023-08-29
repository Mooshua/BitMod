using BitMod.Attributes.Targets;
using BitMod.Commands.Extensions;
using BitMod.Commands.Handlers;
using BitMod.Commands.Sources.Internal;
using BitMod.Events.Core;
using BitMod.Internal.Public;

using Lilikoi.Standard;

using Serilog;

namespace BitMod.Commands.Hosts;

public class StandardInputHost
{

	[Singleton]
	private ILogger _logger;

	[Singleton]
	private PluginInvoker _invoker;

	[BitEvent]
	public async Task OnStandardInput(StandardInputEventArgs ev)
	{
		var source = new StandardInputSource(_logger);
		var input = CommandInput.FromString(source, ev.Contents);

		_invoker.Command(input);
	}

}
