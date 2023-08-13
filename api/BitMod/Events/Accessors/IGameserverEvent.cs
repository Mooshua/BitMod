using BattleBitAPI.Server;

using BitMod.Compatibility;

namespace BitMod.Events.Accessors;

public interface IGameserverEvent
{

	#region IRelevantGameserver

	/// <summary>
	/// Gets the gameserver that this event occured on.
	/// </summary>
	BitServer Server { get; }

	#endregion
}
