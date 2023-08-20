using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player;

public class PlayerRevivedEventArgs : IEventArgs, IResponsiblePlayerEvent
{
	public PlayerRevivedEventArgs(BitServer server, BitPlayer healer, BitPlayer revived)
	{
		Server = server;
		Healer = healer;
		Revived = revived;
	}

	/// <summary>
	/// The player that was revived
	/// </summary>
	public BitPlayer Revived { get; }

	/// <summary>
	/// The player that did the healing
	/// </summary>
	public BitPlayer Healer { get; }

	public BitServer Server { get; }

	public BitPlayer? ResponsiblePlayer => Healer;
}
