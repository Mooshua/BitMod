using BattleBitAPI.Server;

namespace BitMod.Events.Accessors;

public interface IRelevantGameserverAccessor
{

	#region IRelevantGameserver

	/// <summary>
	/// Gets the gameserver that this event occured on.
	/// </summary>
	public GameServer? RelevantGameserver { get; }

	#endregion
}
