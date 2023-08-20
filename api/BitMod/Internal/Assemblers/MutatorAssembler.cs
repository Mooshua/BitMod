using System.Collections.Generic;
using System.Linq;

using BitMod.Internal.Handlers;
using BitMod.Internal.Registries;

using Lilikoi.Compiler.Public;

using Serilog;

namespace BitMod.Internal.Assemblers;

internal class MutatorAssembler : BaseAssembler<MutatorEventRegistry>
{
	private ILogger _logger;

	public MutatorAssembler(ILogger logger)
	{
		_logger = logger;
	}

	public override MutatorEventRegistry Assemble(IEnumerable<LilikoiContainer> containers)
		=> new (containers.Select(container => new SimpleEventHandler(container)).ToList(), _logger);

}
