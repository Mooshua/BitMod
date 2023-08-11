using BitMod.Compatibility;

namespace BitMod.Events.Player;

public class PlayerSpawnedEventArgs
{
	public PlayerSpawnedEventArgs(BitPlayer player)
	{
		Player = player;
	}

	/// <summary>
	/// The player that just spawned.
	/// </summary>
	public BitPlayer Player { get; init; }
}
