using System;
using System.Threading.Tasks;

using BitMod.Plugins.Events;

using Lilikoi.Compiler.Public;

namespace BitMod.Internal.Handlers;

internal class ProducerEventHandler
{

	public ProducerEventHandler(LilikoiContainer container)
	{
		Container = container;
		Priority = container.Get<EventPriority>()?.Priority ?? Byte.MaxValue;
		Compiled = container.Compile<EventInput, Task<Product>>();
	}

	public byte Priority { get; }

	public LilikoiContainer Container { get; }

	public Func<EventInput, Task<Product>> Compiled { get; }

	public Product Invoke(EventInput input)
		=> Compiled(input).Result;
}
