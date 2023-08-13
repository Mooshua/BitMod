using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Server;

public class GameServerReconnectedEventArgs : IEventArgs, IGameserverEvent
{
	public GameServerReconnectedEventArgs(BitServer server)
	{
		Server = server;
	}

	/// <summary>
	/// The server that successfully reconnected
	/// </summary>
	public BitServer Server { get; init; }

	/// <inheritdoc />
	public BitServer RelevantGameserver => Server;
}
