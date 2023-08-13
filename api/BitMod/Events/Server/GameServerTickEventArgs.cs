using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Server;

public class GameServerTickEventArgs : IEventArgs, IGameserverEvent
{
	public GameServerTickEventArgs(BitServer server)
	{
		Server = server;
	}

	/// <summary>
	/// The server that is currently ticking
	/// </summary>
	public BitServer Server { get; init; }

	/// <inheritdoc />
	public BitServer RelevantGameserver => Server;
}
