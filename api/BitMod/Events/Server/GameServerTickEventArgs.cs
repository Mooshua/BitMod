using BattleBitAPI.Server;

using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Server;

public class GameServerTickEventArgs : IEventArgs, IRelevantGameserverAccessor
{
	public GameServerTickEventArgs(GameServer server)
	{
		Server = server;
	}

	/// <summary>
	/// The server that is currently ticking
	/// </summary>
	public GameServer Server { get; init; }

	/// <inheritdoc />
	public GameServer RelevantGameserver => Server;
}
