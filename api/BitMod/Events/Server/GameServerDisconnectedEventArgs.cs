using BattleBitAPI.Server;

namespace BitMod.Events.Server;

public class GameServerDisconnectedEventArgs
{
	public GameServerDisconnectedEventArgs(GameServer server)
	{
		Server = server;
	}

	/// <summary>
	/// The server that was disconnected
	/// </summary>
	public GameServer Server { get; init; }
}
