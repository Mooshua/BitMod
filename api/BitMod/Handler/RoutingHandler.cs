using System.Net;
using System.Threading.Tasks;

using BattleBitAPI.Common;
using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Events.Server;
using BitMod.Internal.Public;

using Serilog;

namespace BitMod.Handler;

public class RoutingHandler : BaseHandler<BitPlayer, BitServer>
{
	private PluginInvoker _invoker;
	private ILogger _logger;

	public RoutingHandler(ServerListener<BitPlayer, BitServer> server, PluginInvoker invoker, ILogger logger) : base(server)
	{
		_invoker = invoker;
		_logger = logger;
	}

	public override async Task<bool> OnGameServerConnecting(IPAddress arg)
		=> _invoker.Hook(new GameServerConnectingEventArgs(arg), false);

	public override BitServer OnCreatingGameServerInstance(IPAddress address, ushort port)
		=> new RoutingGameserver(_invoker);

	public override BitPlayer OnCreatingPlayerInstance(ulong steamid)
		=> new BitPlayer();

	public override void OnLog(LogLevel name, string value, object? sender)
		=> _logger.Verbose("[API] {@Level} {@Value}", name, value);
}
