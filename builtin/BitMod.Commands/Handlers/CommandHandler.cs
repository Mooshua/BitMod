using BitMod.Internal;
using BitMod.Plugins.Events;

using Lilikoi.Compiler.Public;

namespace BitMod.Commands.Handlers;

internal class CommandHandler
{
	public CommandHandler(LilikoiContainer container)
	{
		Container = container;
		Compiled = container.Compile<EventInput, Task<Directive>>();
	}

	public LilikoiContainer Container { get; }

	public Func<EventInput, Task<Directive>> Compiled { get; }

	public Directive Invoke(EventInput input)
		=> Compiled(input).Result;
}
