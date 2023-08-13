using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player;

public class PlayerSpawnedEventArgs : IEventArgs, IResponsiblePlayerEvent
{
	public PlayerSpawnedEventArgs(BitServer server, BitPlayer player)
	{
		Player = player;
		Server = server;
	}

	/// <summary>
	/// The player that just spawned.
	/// </summary>
	public BitPlayer Player { get; init; }

	/// <inheritdoc />
	public BitPlayer ResponsiblePlayer => Player;

	/// <inheritdoc />
	public BitServer Server { get; }
}
