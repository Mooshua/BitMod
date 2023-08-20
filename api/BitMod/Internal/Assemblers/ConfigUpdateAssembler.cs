using BitMod.Internal.Handlers;
using BitMod.Internal.Registries;
using BitMod.Router;

using Lilikoi.Compiler.Public;

using Serilog;

namespace BitMod.Internal.Assemblers;

internal class ConfigUpdateAssembler : IRouteAssembler<ConfigUpdatedRegistry, string>
{
	private ILogger _logger;

	public ConfigUpdateAssembler(ILogger logger)
	{
		_logger = logger;
	}

	public string Bin(LilikoiContainer container)
		=> container.Get<StringRouterDirectives>().Route;

	public ConfigUpdatedRegistry Assemble(IEnumerable<LilikoiContainer> containers)
		=> new (containers.Select(container => new SimpleEventHandler(container)).ToList(), _logger);

}
