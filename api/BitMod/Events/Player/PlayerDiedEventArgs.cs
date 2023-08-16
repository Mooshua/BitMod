using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player;

public class PlayerDiedEventArgs : IEventArgs, IResponsiblePlayerEvent
{
	public PlayerDiedEventArgs(BitServer server, BitPlayer player)
	{
		Player = player;
		Server = server;
	}

	/// <summary>
	/// The player that died.
	/// For information on the killer, hook PlayerKilledPlayer instead.
	/// </summary>
	public BitPlayer Player { get; }

	/// <inheritdoc />
	public BitPlayer ResponsiblePlayer => Player;

	/// <inheritdoc />
	public BitServer Server { get; }
}
