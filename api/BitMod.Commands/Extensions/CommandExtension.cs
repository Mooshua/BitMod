using BitMod.Extensions.Commands;
using BitMod.Plugins.Extensions;

using Lilikoi.Context;
using Lilikoi.Standard;

using Serilog;

namespace BitMod.Extensions;

public class CommandExtension : IExtension
{

	public string Name => "bitmod_commands";

	public void Register(Mount mount)
	{
		var logger = mount.Get<ILogger>();

		mount.Store( new CommandContext(mount, logger) );
		logger.Information("[BitMod Commands] Loaded BitMod commands! Weeee!");
	}

	public void Unregister(Mount mount)
	{
		mount.Store<CommandContext>(null);
	}
}
