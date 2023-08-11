using CommunityServerAPI.BattleBitAPI.Server;

namespace BitMod.Events.Server;

public class GameServerReconnectedEventArgs
{
	public GameServerReconnectedEventArgs(GameServer server)
	{
		Server = server;
	}

	/// <summary>
	/// The server that successfully reconnected
	/// </summary>
	public GameServer Server { get; init; }
}
