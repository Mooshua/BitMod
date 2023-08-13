using System.Net;
using System.Threading.Tasks;

using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Events.Server;
using BitMod.Internal.Public;

namespace BitMod.Handler;

public class RoutingHandler : BaseHandler<BitPlayer, BitServer>
{
	private PluginInvoker _invoker;

	public RoutingHandler(ServerListener<BitPlayer, BitServer> server, PluginInvoker invoker) : base(server)
	{
		_invoker = invoker;
	}

	public override async Task<bool> OnGameServerConnecting(IPAddress arg)
		=> _invoker.Hook(new GameServerConnectingEventArgs(arg), false);
}
