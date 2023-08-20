using BattleBitAPI.Common;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player;

public class PlayerChangingTeamEventArgs : IHookArgs, IResponsiblePlayerEvent
{
	public PlayerChangingTeamEventArgs(BitServer server, BitPlayer player, Team team)
	{
		Server = server;
		Player = player;
		Team = team;
	}

	public BitPlayer Player { get; }

	public Team Team { get; }

	public BitServer Server { get; }

	public BitPlayer? ResponsiblePlayer => Player;
}
