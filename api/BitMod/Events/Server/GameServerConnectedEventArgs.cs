using BattleBitAPI.Server;

using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Server;

public class GameServerConnectedEventArgs : IEventArgs, IRelevantGameserverAccessor
{
	public GameServerConnectedEventArgs(GameServer server)
	{
		Server = server;
	}

	/// <summary>
	/// The server that successfully connected
	/// </summary>
	public GameServer Server { get; init; }

	/// <inheritdoc />
	public GameServer RelevantGameserver => Server;
}
