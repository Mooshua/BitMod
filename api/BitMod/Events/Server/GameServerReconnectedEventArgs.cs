using BattleBitAPI.Server;

using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Server;

public class GameServerReconnectedEventArgs : IEventArgs, IRelevantGameserverAccessor
{
	public GameServerReconnectedEventArgs(GameServer server)
	{
		Server = server;
	}

	/// <summary>
	/// The server that successfully reconnected
	/// </summary>
	public GameServer Server { get; init; }

	/// <inheritdoc />
	public GameServer RelevantGameserver => Server;
}
