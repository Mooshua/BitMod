using BattleBitAPI.Server;

using BitMod.Compatibility;

namespace BitMod.Events.Accessors;

public interface IResponsiblePlayerEvent : IGameserverEvent
{
	#region IResponsiblePlayer
	/// <summary>
	/// Gets the player responsible for the event.
	/// For example, when a player is reported, this will be the player
	/// that reported that player.
	/// </summary>
	public BitPlayer? ResponsiblePlayer { get; }

	#endregion
}
