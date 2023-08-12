using BitMod.Internal.Handlers;
using BitMod.Internal.Registries;

using Lilikoi.Compiler.Public;

using Serilog;

namespace BitMod.Internal.Assemblers;

internal class ProducerAssembler : BaseAssembler<ProducerEventRegistry>
{
	private ILogger _logger;

	public ProducerAssembler(ILogger logger)
	{
		_logger = logger;
	}

	public override ProducerEventRegistry Assemble(IEnumerable<LilikoiContainer> containers)
		=> new (containers.Select(container => new ProducerEventHandler(container)).ToList(), _logger);
}
