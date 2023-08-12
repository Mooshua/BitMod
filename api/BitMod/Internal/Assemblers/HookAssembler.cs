using BitMod.Internal.Handlers;
using BitMod.Internal.Registries;
using BitMod.Router;

using Lilikoi.Compiler.Public;

using Serilog;

namespace BitMod.Internal.Assemblers;

internal class HookAssembler : BaseAssembler<HookEventRegistry>
{
	private ILogger _logger;

	public HookAssembler(ILogger logger)
	{
		_logger = logger;
	}

	public override HookEventRegistry Assemble(IEnumerable<LilikoiContainer> containers)
		=> new (containers.Select(container => new HookEventHandler(container)).ToList(), _logger);
}
