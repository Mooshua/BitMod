using BitMod.Flags.Configuration;
using BitMod.Plugins.Extensions;
using BitMod.Public;

using Lilikoi.Context;

using Serilog;

namespace BitMod.Flags.Public;

public class FlagExtension : IExtension
{
	public string Name => "bitmod_flags::FlagExtension";

	public void Register(Mount mount)
	{
		var cfg = mount.Get<IConfigurationSystem>();
		var cfgObject = cfg.Get(FlagFile.NAME);
		var flagFile = new FlagFile(cfgObject, mount.Get<ILogger>(), cfg);
		mount.Store(flagFile);
	}

	public void Unregister(Mount mount)
	{
		mount.Store<FlagFile>(null);
	}
}
