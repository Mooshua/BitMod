using BitMod.Internal;
using BitMod.Plugins.Events;

using Lilikoi.Compiler.Public;

namespace BitMod.Commands.Handlers;

internal class CommandHandler
{
	public CommandHandler(LilikoiContainer container)
	{
		Container = container;
		Compiled = container.Compile<EventInput, Task>();
	}

	public LilikoiContainer Container { get; }

	public Func<EventInput, Task> Compiled { get; }

	public void Invoke(EventInput input)
		=> Compiled(input);
}
