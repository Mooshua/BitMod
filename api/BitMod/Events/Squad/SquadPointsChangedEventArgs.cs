using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Squad;

public class SquadPointsChangedEventArgs : IGameserverEvent, IEventArgs
{
	public SquadPointsChangedEventArgs(BitServer server, Squad<BitPlayer> squad, int points)
	{
		Squad = squad;
		Server = server;
		Points = points;
	}

	/// <summary>
	/// The number of points the squad has at the time of the event
	/// </summary>
	public int Points { get; }


	public Squad<BitPlayer> Squad { get; }

	public BitServer Server { get; }
}
