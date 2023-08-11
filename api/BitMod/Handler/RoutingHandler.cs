using BitMod.Compatibility;

using CommunityServerAPI.BattleBitAPI.Server;

namespace BitMod.Handler;

public class RoutingHandler : DefaultHandler<BitPlayer>
{
	public RoutingHandler(ServerListener<BitPlayer> server) : base(server)
	{
	}

	public override Task OnPlayerDied(BitPlayer player)
	{
		return base.OnPlayerDied(player);
	}
}
