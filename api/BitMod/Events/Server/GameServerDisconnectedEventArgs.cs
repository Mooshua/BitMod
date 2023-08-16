using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Server;

public class GameServerDisconnectedEventArgs : IEventArgs, IGameserverEvent
{
	public GameServerDisconnectedEventArgs(BitServer server)
	{
		Server = server;
	}

	/// <summary>
	/// The server that was disconnected
	/// </summary>
	public BitServer Server { get; }

	/// <inheritdoc />
	public BitServer RelevantGameserver => Server;
}
