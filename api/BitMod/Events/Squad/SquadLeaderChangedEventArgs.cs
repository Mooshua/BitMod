using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Squad;

public class SquadLeaderChangedEventArgs : IEventArgs, IGameserverEvent, IResponsiblePlayerEvent
{
	public SquadLeaderChangedEventArgs(BitServer server, Squad<BitPlayer> squad, BitPlayer leader)
	{
		Server = server;
		Leader = leader;
		Squad = squad;
	}

	/// <summary>
	/// The squad that had it's leadership changed
	/// </summary>
	public Squad<BitPlayer> Squad { get; }

	/// <summary>
	/// The new leader
	/// </summary>
	public BitPlayer Leader { get; }

	/// <summary>
	/// The server this event happened on
	/// </summary>
	public BitServer Server { get; }

	public BitPlayer? ResponsiblePlayer => Leader;
}
