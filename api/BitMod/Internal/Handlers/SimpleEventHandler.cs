using Lilikoi.Compiler.Public;

namespace BitMod.Internal.Handlers;

internal class SimpleEventHandler
{
	public SimpleEventHandler(LilikoiContainer container)
	{
		Container = container;
		Compiled = container.Compile<EventInput, Task>();
	}

	public LilikoiContainer Container { get; }

	public Func<EventInput, Task> Compiled { get; }

	public void Invoke(EventInput input)
		=> Compiled(input);
}
