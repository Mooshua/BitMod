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
		var logger = mount.Get<ILogger>();
		var cfgObject = cfg.Get(FlagFile.NAME);
		var flagFile = new FlagFile(cfgObject, logger, cfg);

		logger.Information("Registering FlagFile");
		mount.Store<FlagFile>(flagFile);
	}

	public void Unregister(Mount mount)
	{
		var logger = mount.Get<ILogger>();
		logger.Information("Unregistering FlagFile");

		mount.Store<FlagFile>(null);
	}
}
