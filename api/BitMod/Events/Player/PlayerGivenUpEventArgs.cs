using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player;

public class PlayerGivenUpEventArgs : IEventArgs, IResponsiblePlayerEvent
{
	public PlayerGivenUpEventArgs(BitServer server, BitPlayer player)
	{
		Player = player;
		Server = server;
	}

	public BitPlayer Player { get; }

	public BitServer Server { get; }

	public BitPlayer? ResponsiblePlayer => Player;
}
