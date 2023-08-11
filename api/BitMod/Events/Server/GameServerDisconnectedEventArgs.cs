using CommunityServerAPI.BattleBitAPI.Server;

namespace BitMod.Events.Server;

public class GameServerDisconnectedEventArgs
{
	/// <summary>
	/// The server that was disconnected
	/// </summary>
	public GameServer Server { get; init; }
}
