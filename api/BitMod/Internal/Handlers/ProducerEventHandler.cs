using BitMod.Plugins.Events;

using Lilikoi.Compiler.Public;

namespace BitMod.Internal.Handlers;

internal class ProducerEventHandler
{

	public ProducerEventHandler(LilikoiContainer container, byte priority)
	{
		Container = container;
		Priority = priority;
		Compiled = container.Compile<EventInput, Task<Product>>();
	}

	public byte Priority { get; }

	public LilikoiContainer Container { get; }

	public Func<EventInput, Task<Product>> Compiled { get; }

	public Product Invoke(EventInput input)
		=> Compiled(input).Result;
}
