using BitMod.Commands.Sources;
using BitMod.Compatibility;
using BitMod.Events.Base;

namespace BitMod.Moderation.Events;

public class PlayerBannedEventArgs : IEventArgs
{

	public PlayerBannedEventArgs(ulong steamId, string name, TimeSpan duration, ICommandSource invoker, string reason)
	{
		SteamID = steamId;
		Name = name;
		Duration = duration;
		Invoker = invoker;
		Reason = reason;
	}

	/// <summary>
	/// The SteamID of the player who was banned
	/// </summary>
	public ulong SteamID { get; }

	/// <summary>
	/// The name of the player who was banned
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// The amount of time this player should be banned for.
	/// </summary>
	public TimeSpan Duration { get; }

	/// <summary>
	/// The player or source that caused this ban to occur.
	/// Note that this is not always an admin--eg for votekicks,
	/// this will be the person who started the kick
	/// </summary>
	public ICommandSource Invoker { get; }

	public string Reason { get; }
}
