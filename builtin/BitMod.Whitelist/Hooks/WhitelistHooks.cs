using System.Net;

using BitMod.Attributes.Injects;
using BitMod.Attributes.Targets;
using BitMod.Configuration.Model;
using BitMod.Events.Config;
using BitMod.Events.Meta;
using BitMod.Events.Server;
using BitMod.Plugins.Events;
using BitMod.Whitelist.Adapter;

using Lilikoi.Standard;

using Serilog;

namespace BitMod.Whitelist.Hooks;

public class WhitelistHooks
{



	[Config("whitelist")]
	private IConfigObject _whitelist;

	[Singleton]
	private ILogger _logger;

	[BitConfigUpdate("whitelist")]
	public async Task ServerConfigUpdate(ConfigUpdatedEventArgs ev)
	{
		var config = new WhitelistConfigAdapter(ev.Config);

		_logger.Information("Updated whitelist with new allowed IPs: {@List}",
			config.GetAllowedAddresses().Select(x => x.ToString()));
	}

	[BitHook(Priority.LOW)]
	public async Task<Directive> ServerConnectionRequest(GameServerConnectingEventArgs ev)
	{
		var config = new WhitelistConfigAdapter(_whitelist);

		foreach (IPAddress allowedAddress in config.GetAllowedAddresses())
		{
			if (allowedAddress.Equals(ev.IPAddress) == true)
			{
				_logger.Information("[BitMod Whitelist] Allowing IP {@Remote} to connect with rule {@IP}", ev.IPAddress.ToString(), allowedAddress.ToString());
				return Directive.Allow;
			}
		}

		_logger.Warning("[BitMod Whitelist] Rejecting {Remote}", ev.IPAddress.ToString());
		return Directive.Neutral;
	}
}
