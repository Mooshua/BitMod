using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player;

public class PlayerDiedEventArgs : IEventArgs, IResponsiblePlayerAccessor
{
	public PlayerDiedEventArgs(BitPlayer player)
	{
		Player = player;
	}

	/// <summary>
	/// The player that died.
	/// For information on the killer, hook PlayerKilledPlayer instead.
	/// </summary>
	public BitPlayer Player { get; init; }

	/// <inheritdoc />
	public BitPlayer ResponsiblePlayer => Player;
}
