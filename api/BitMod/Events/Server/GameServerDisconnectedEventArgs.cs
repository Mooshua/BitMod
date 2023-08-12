using BattleBitAPI.Server;

using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Server;

public class GameServerDisconnectedEventArgs : IEventArgs, IRelevantGameserverAccessor
{
	public GameServerDisconnectedEventArgs(GameServer server)
	{
		Server = server;
	}

	/// <summary>
	/// The server that was disconnected
	/// </summary>
	public GameServer Server { get; init; }

	/// <inheritdoc />
	public GameServer RelevantGameserver => Server;
}
