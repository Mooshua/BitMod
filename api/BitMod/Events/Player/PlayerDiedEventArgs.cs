using BitMod.Compatibility;

namespace BitMod.Events.Player;

public class PlayerDiedEventArgs
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
}
