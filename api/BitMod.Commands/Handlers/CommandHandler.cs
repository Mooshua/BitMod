using BitMod.Internal;
using BitMod.Plugins.Events;

using Lilikoi.Compiler.Public;

namespace BitMod.Commands.Handlers;

internal class CommandHandler
{
	public CommandHandler(LilikoiContainer container)
	{
		Container = container;
		Compiled = container.Compile<CommandInput, Task<Directive>>();
	}

	public LilikoiContainer Container { get; }

	public Func<CommandInput, Task<Directive>> Compiled { get; }

	public Directive Invoke(CommandInput input)
		=> Compiled(input).Result;
}
