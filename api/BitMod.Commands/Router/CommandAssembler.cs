using BitMod.Commands.Handlers;
using BitMod.Router;

using Lilikoi.Compiler.Public;

using Serilog;

namespace BitMod.Commands.Router;

internal class CommandAssembler : IRouteAssembler<CommandHandlerRegistry, string>
{
	private ILogger _logger;

	public string Bin(LilikoiContainer container)
		=> container.Get<CommandMetadata>().Command;

	public CommandHandlerRegistry Assemble(IEnumerable<LilikoiContainer> containers)
		=> new (containers.Select(container => new CommandHandler(container)).ToList(), _logger);
}
