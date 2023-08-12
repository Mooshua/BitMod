using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player;

public class PlayerDisconnectedEventArgs : IEventArgs, IResponsiblePlayerAccessor
{
	public PlayerDisconnectedEventArgs(BitPlayer player)
	{
		Player = player;
	}

	/// <summary>
	/// The player that just disconnected.
	/// </summary>
	public BitPlayer Player { get; init; }

	/// <inheritdoc />
	public BitPlayer ResponsiblePlayer => Player;
}
