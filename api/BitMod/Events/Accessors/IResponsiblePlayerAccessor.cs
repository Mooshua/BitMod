using BattleBitAPI.Server;

using BitMod.Compatibility;

namespace BitMod.Events.Accessors;

public interface IResponsiblePlayerAccessor : IRelevantGameserverAccessor
{
	#region IResponsiblePlayer
	/// <summary>
	/// Gets the player responsible for the event.
	/// For example, when a player is reported, this will be the player
	/// that reported that player.
	/// </summary>
	public BitPlayer? ResponsiblePlayer { get; }

	/// <inheritdoc />
	GameServer IRelevantGameserverAccessor.RelevantGameserver => ResponsiblePlayer.GameServer;

	#endregion
}
