using CommunityServerAPI.BattleBitAPI.Server;

namespace BitMod.Events.Server;

public class GameServerConnectedEventArgs
{
	public GameServerConnectedEventArgs(GameServer server)
	{
		Server = server;
	}

	/// <summary>
	/// The server that successfully connected
	/// </summary>
	public GameServer Server { get; init; }
}
