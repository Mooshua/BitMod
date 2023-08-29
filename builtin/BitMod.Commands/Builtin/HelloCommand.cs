using BitMod.Commands.Attributes;
using BitMod.Commands.Sources;

namespace BitMod.Commands.Builtin;

public class HelloCommand
{

	[BitCommand("hello", "Say Hello")]
	public async Task OnHello(ICommandSource source)
		=> source.Reply("[BitMod] Hello, there!");

}
