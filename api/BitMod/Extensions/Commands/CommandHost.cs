using BitMod.Attributes.Targets;
using BitMod.Events.Player;
using BitMod.Plugins.Events;

using Lilikoi.Standard;

namespace BitMod.Extensions.Commands;

public class CommandHost
{

	[Singleton]
	private CommandContext _context;

	/// <summary>
	/// This hook receives chat messages, and if they begin with
	/// a command prefix ("/" and "!"), will route them into
	/// the command system.
	/// </summary>
	/// <param name="ev"></param>
	/// <returns></returns>
	[BitHook(Priority.HIGH)]
	public Task<Directive> OnChatMessage(PlayerTypedMessageEventArgs ev)
	{
		var message = ev.Message;

		if (message.StartsWith('/') || message.StartsWith("!"))
		{
			_context.Invoke(ev.Player, ev.Message);

			return Task.FromResult(Directive.Disallow);
		}

		return Task.FromResult(Directive.Neutral);
	}
}
