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

internal class HookEventContext : BaseEventContext<HookEventHandler>
{
	private HookEventRegistry _currentRegistry;

	public HookEventContext(ILogger logger) : base(logger)
	{
		_currentRegistry = new HookEventRegistry(new List<HookEventHandler>(), _logger);
	}

	protected override void Rebuild()
	{
		_currentRegistry = new HookEventRegistry(_handlers
			.SelectMany(kv => kv.Value)
			.ToList(), _logger);
	}

	public Directive Invoke(EventInput input)
		=> _currentRegistry.Invoke(input);

	public static List<LilikoiContainer> Scan(EventRegistrationContext ctx, MethodInfo methodInfo, Func<Mount> mount)
		=> Scanner.Scan<HookRegistrationContext, EventInput, Task<Directive>>(new HookRegistrationContext(ctx), methodInfo, mount);
}
