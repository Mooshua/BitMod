using CommunityServerAPI.BattleBitAPI.Server;

namespace BitMod.Events.Server;

public class GameServerTickEventArgs
{
	public GameServerTickEventArgs(GameServer server)
	{
		Server = server;
	}

	/// <summary>
	/// The server that is currently ticking
	/// </summary>
	public GameServer Server { get; init; }
}
