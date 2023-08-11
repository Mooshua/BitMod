using System.Reflection;

using BitMod.Internal.Handlers;
using BitMod.Internal.LilikoiRouting;
using BitMod.Internal.Registries;

using Lilikoi.Compiler.Public;
using Lilikoi.Context;
using Lilikoi.Scan;

using Serilog;

namespace BitMod.Internal.Contexts;

internal class SimpleEventContext : BaseEventContext<SimpleEventHandler>
{

	private SimpleEventRegistry _currentRegistry;


	public SimpleEventContext(ILogger logger) : base(logger)
	{
		_currentRegistry = new SimpleEventRegistry(new List<SimpleEventHandler>(), _logger);
	}

	protected override void Rebuild()
	{
		_currentRegistry = new SimpleEventRegistry(_handlers
			.SelectMany(kv => kv.Value)
			.ToList(), _logger);
	}

	public void Invoke(EventInput input)
		=> _currentRegistry.Invoke(input);

	public static List<LilikoiContainer> Scan(EventRegistrationContext ctx, MethodInfo methodInfo, Func<Mount> mount)
		=> Scanner.Scan<SimpleRegistrationContext, EventInput, Task>(new SimpleRegistrationContext(ctx), methodInfo, mount);
}
