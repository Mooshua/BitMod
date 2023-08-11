using BitMod.Compatibility;

namespace BitMod.Events.Player;

public class PlayerConnectedEventArgs
{
	public PlayerConnectedEventArgs(BitPlayer player)
	{
		Player = player;
	}

	/// <summary>
	/// The player that connected
	/// </summary>
	public BitPlayer Player { get; init; }
}
