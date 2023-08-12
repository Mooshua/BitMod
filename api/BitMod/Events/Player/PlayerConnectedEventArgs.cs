using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player;

public class PlayerConnectedEventArgs : IEventArgs, IResponsiblePlayerAccessor
{
	public PlayerConnectedEventArgs(BitPlayer player)
	{
		Player = player;
	}

	/// <summary>
	/// The player that connected
	/// </summary>
	public BitPlayer Player { get; init; }

	/// <inheritdoc />
	public BitPlayer ResponsiblePlayer => Player;
}
