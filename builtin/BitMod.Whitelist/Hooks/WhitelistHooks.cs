using System.Net;

using BitMod.Attributes.Injects;
using BitMod.Attributes.Targets;
using BitMod.Events.Server;
using BitMod.Flags.Attribute;
using BitMod.Plugins.Events;
using BitMod.Whitelist.Configuration;

using Lilikoi.Standard;

using Serilog;

namespace BitMod.Whitelist.Hooks;

public class WhitelistHooks
{

	[Config("whitelist")]
	private WhitelistFile _whitelist;

	[Singleton]
	private ILogger _logger;

	[BitHook(Priority.LOW)]
	private async Task<Directive> ServerConnectionRequest(GameServerConnectingEventArgs ev)
	{
		_logger.Information("Using file {@Config}", _whitelist);
		foreach (IPAddress allowedConnection in _whitelist.Parse(_logger))
		{
			if (allowedConnection.GetAddressBytes().SequenceEqual(ev.IPAddress.GetAddressBytes()))
			{
				_logger.Information("[BitMod Whitelist] Allowing IP {@Remote} to connect with rule {@IP}", ev.IPAddress.ToString(), allowedConnection.ToString());
				return Directive.Allow;
			}
		}

		_logger.Warning("[BitMod Whitelist] Rejecting {Remote}", ev.IPAddress.ToString());
		return Directive.Neutral;
	}
}
