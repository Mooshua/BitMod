using BitMod.Compatibility;
using BitMod.Extensions.Commands.Attributes;
using BitMod.Plugins.Events;

namespace BitMod.Commands;

public class BitModCommands
{
	[Command("hello", "Says hello to the player!", "hi")]
	public async Task<Completion> HelpCommand(BitPlayer sender)
	{
		sender.Message(@"Hello from BitMod!");

		return Completion.Completed;
	}
}
