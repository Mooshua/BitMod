using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Internal.Public;

namespace BitMod.Handler;

public class RoutingHandlerFactory : GameServerFactory<BitServer, BitPlayer>
{
	private PluginInvoker _invoker;

	public RoutingHandlerFactory(PluginInvoker invoker)
	{
		_invoker = invoker;
	}

	public override BitServer Create()
		=> new RoutingGameserver(_invoker);
}
