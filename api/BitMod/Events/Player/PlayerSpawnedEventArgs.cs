using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player;

public class PlayerSpawnedEventArgs : IEventArgs, IResponsiblePlayerAccessor
{
	public PlayerSpawnedEventArgs(BitPlayer player)
	{
		Player = player;
	}

	/// <summary>
	/// The player that just spawned.
	/// </summary>
	public BitPlayer Player { get; init; }

	/// <inheritdoc />
	public BitPlayer ResponsiblePlayer => Player;
}
