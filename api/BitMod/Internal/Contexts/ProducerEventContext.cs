using System.Reflection;

using BitMod.Internal.Handlers;
using BitMod.Internal.LilikoiRouting;
using BitMod.Internal.Registries;
using BitMod.Plugins.Events;

using Lilikoi.Compiler.Public;
using Lilikoi.Context;
using Lilikoi.Scan;

using Serilog;

namespace BitMod.Internal.Contexts;

internal class ProducerEventContext : BaseEventContext<ProducerEventHandler>
{
	private ProducerEventRegistry _currentRegistry;

	public ProducerEventContext(ILogger logger) : base(logger)
	{
		_currentRegistry = new ProducerEventRegistry(new List<ProducerEventHandler>(), _logger);
	}

	protected override void Rebuild()
	{
		_currentRegistry = new ProducerEventRegistry(_handlers
			.SelectMany(kv => kv.Value)
			.ToList(), _logger);
	}

	public Product Invoke(EventInput input)
		=> _currentRegistry.Invoke(input);

	public static List<LilikoiContainer> Scan(EventRegistrationContext ctx, MethodInfo methodInfo, Func<Mount> mount)
		=> Scanner.Scan<ProducerRegistrationContext, EventInput, Task<Product>>( new ProducerRegistrationContext( ctx ), methodInfo, mount);

}
