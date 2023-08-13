using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Server;

public class GameServerConnectedEventArgs : IEventArgs, IGameserverEvent
{
	public GameServerConnectedEventArgs(BitServer server)
	{
		Server = server;
	}

	/// <summary>
	/// The server that successfully connected
	/// </summary>
	public BitServer Server { get; init; }

	/// <inheritdoc />
	public BitServer RelevantGameserver => Server;
}
