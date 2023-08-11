using BitMod.Plugins.Events;

using Lilikoi.Compiler.Public;

namespace BitMod.Internal.Handlers;

internal class HookEventHandler
{
	public HookEventHandler(LilikoiContainer container, byte priority)
	{
		Container = container;
		Priority = priority;
		Compiled = container.Compile<EventInput, Task<Directive>>();
	}

	public byte Priority { get; }

	public LilikoiContainer Container { get; }

	public Func<EventInput, Task<Directive>> Compiled { get; }

	public Directive Invoke(EventInput input)
		=> Compiled(input).Result;
}
