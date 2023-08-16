using System.Net;

using BitMod.Attributes.Injects;
using BitMod.Attributes.Targets;
using BitMod.Configuration.Model;
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

	[BitHook(Priority.LOW)]
	private async Task<Directive> ServerConnectionRequest(GameServerConnectingEventArgs ev)
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
