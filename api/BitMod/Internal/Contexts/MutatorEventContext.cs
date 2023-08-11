using System.Reflection;

using BitMod.Internal.Handlers;
using BitMod.Internal.LilikoiRouting;
using BitMod.Internal.Registries;

using Lilikoi.Compiler.Public;
using Lilikoi.Context;
using Lilikoi.Scan;

using Serilog;

namespace BitMod.Internal.Contexts;

internal class MutatorEventContext : BaseEventContext<MutatorEventHandler>
{

	private MutatorEventRegistry _currentRegistry;

	public MutatorEventContext(ILogger logger) : base(logger)
	{
		_currentRegistry = new MutatorEventRegistry(new List<MutatorEventHandler>(), _logger);
	}

	protected override void Rebuild()
	{
		_currentRegistry = new MutatorEventRegistry(_handlers
			.SelectMany(kv => kv.Value)
			.ToList(), _logger);
	}

	public void Invoke(EventInput input)
		=> _currentRegistry.Invoke(input);

	public static List<LilikoiContainer> Scan(EventRegistrationContext ctx, MethodInfo methodInfo, Func<Mount> mount)
		=> Scanner.Scan<MutatorRegistrationContext, EventInput, Task>(new MutatorRegistrationContext(ctx), methodInfo, mount);
}
