using BitMod.Plugins.Events;

using Lilikoi.Compiler.Public;

namespace BitMod.Internal.Handlers;

internal class HookEventHandler
{
	public HookEventHandler(LilikoiContainer container)
	{
		Container = container;
		Priority = container.Get<EventPriority>()?.Priority ?? Byte.MaxValue;
		Compiled = container.Compile<EventInput, Task<Directive>>();
	}

	public byte Priority { get; }

	public LilikoiContainer Container { get; }

	public Func<EventInput, Task<Directive>> Compiled { get; }

	public Directive Invoke(EventInput input)
		=> Compiled(input).Result;
}
