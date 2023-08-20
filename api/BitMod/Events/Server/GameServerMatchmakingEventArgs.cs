using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;
using BitMod.Events.Result;

namespace BitMod.Events.Server;

public class GameServerMatchmakingEventArgs : IProducerArgs<PlayerMatchmaking>, IGameserverEvent
{
	public GameServerMatchmakingEventArgs(BitServer server, ulong steamId)
	{
		SteamID = steamId;
		Server = server;
	}

	public ulong SteamID { get; }

	public BitServer Server { get; }
}
