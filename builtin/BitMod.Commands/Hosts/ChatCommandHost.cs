using BitMod.Attributes.Targets;
using BitMod.Commands.Extensions;
using BitMod.Commands.Handlers;
using BitMod.Commands.Sources.Internal;
using BitMod.Compatibility;
using BitMod.Events.Player;
using BitMod.Internal.Public;
using BitMod.Plugins.Events;

using Lilikoi.Standard;

using Serilog;

namespace BitMod.Commands.Hosts;

public class ChatCommandHost
{
	[Singleton]
	private PluginInvoker _invoker;

	[Singleton]
	private ILogger _logger;

	[BitHook(Priority.HIGH)]
	public Task<Directive> OnChatMessage(PlayerTypedMessageEventArgs ev, BitServer server, BitPlayer sender)
	{
		if (ev.Message.StartsWith("!") || ev.Message.StartsWith("/"))
		{
			//	This is a command (starts with ! or /)
			//	Send to command handler and prevent it from being shown in chat.
			var withoutPrefix = ev.Message.Substring(1);

			var source = new InGameSource(server, sender, _logger);
			var input = CommandInput.FromString(source, withoutPrefix);

			_logger.Information("Player {@Name} {@SteamId} used command: {@Command} {@Args} (From string {@Message})",
				sender.Name, sender.SteamID, input.Command, input.Arguments, ev.Message);
			_invoker.Command( input );

			return Task.FromResult(Directive.Disallow);
		}

		return Task.FromResult(Directive.Neutral);
	}

}
