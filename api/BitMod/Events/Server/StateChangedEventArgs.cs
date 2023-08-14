using BattleBitAPI.Common;
using BattleBitAPI.Common.Enums;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Server;

public class StateChangedEventArgs : IEventArgs, IGameserverEvent
{
	public StateChangedEventArgs(BitServer server, GameState old, GameState @new)
	{
		Server = server;
		Old = old;
		New = @new;
	}

	public GameState Old { get; }

	public GameState New { get; }

	public BitServer Server { get; }
}
