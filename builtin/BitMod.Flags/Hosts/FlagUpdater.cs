using BitMod.Attributes.Injects;
using BitMod.Attributes.Mutators;
using BitMod.Attributes.Targets;
using BitMod.Configuration.Model;
using BitMod.Events.Config;
using BitMod.Flags.Configuration;
using BitMod.Public;

using Lilikoi.Context;
using Lilikoi.Standard;

using Serilog;

namespace BitMod.Flags.Hosts;

public class FlagUpdater
{

	[Meta]
	private Mount _bitMod;

	[Singleton]
	private IConfigurationSystem _configurationSystem;

	/// <summary>
	/// Invoked every time the config file updates
	/// </summary>
	/// <param name="newFile"></param>
	[BitConfigUpdate(FlagFile.NAME)]
	[Log]
	public async Task OnUpdate(ConfigUpdatedEventArgs newFile, ILogger logger)
	{
		logger.Debug("Received new FlagFile update!");
		_bitMod.Store(new FlagFile(newFile.Config, logger, _configurationSystem));
	}

}
