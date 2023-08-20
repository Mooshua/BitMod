using BattleBitAPI.Common;

namespace BitMod.Events.Result;

public class PlayerMatchmaking
{
	public Squads Squad { get; set; } = Squads.NoSquad;

	public Team Team { get; set; } = Team.None;
}
