using BitMod.Compatibility;

namespace BitMod.Events.Player;

public class PlayerDisconnectedEventArgs
{
	public PlayerDisconnectedEventArgs(BitPlayer player)
	{
		Player = player;
	}

	/// <summary>
	/// The player that just disconnected.
	/// </summary>
	public BitPlayer Player { get; init; }
}
