using System.Collections.Generic;
using System.Linq;

using BitMod.Internal.Handlers;
using BitMod.Internal.Registries;
using BitMod.Router;

using Lilikoi.Compiler.Public;

using Serilog;

namespace BitMod.Internal.Assemblers;

internal class EventAssembler : BaseAssembler<SimpleEventRegistry>
{
	private ILogger _logger;

	public EventAssembler(ILogger logger)
	{
		_logger = logger;
	}

	public override SimpleEventRegistry Assemble(IEnumerable<LilikoiContainer> containers)
		=> new (containers.Select(container => new SimpleEventHandler(container)).ToList(), _logger);

}
