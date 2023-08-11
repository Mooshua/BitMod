using BitMod.Extensions.Commands;
using BitMod.Plugins.Extensions;

using Lilikoi.Context;

using Serilog;

namespace BitMod.Extensions;

public class CommandExtension : IExtension
{
	public string Name => "BitMod::Commands";

	public void Register(Mount mount)
	{
		mount.Store( new CommandContext(mount, mount.Get<ILogger>()) );
	}

	public void Unregister(Mount mount)
	{
		mount.Store<CommandContext>(null);
	}
}
