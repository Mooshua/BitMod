using BitMod.Commands.Sources;
using BitMod.Compatibility;

namespace BitMod.Moderation.Votekick;

public class VotekickState
{

	public VotekickState(BitPlayer target, ICommandSource creator)
	{
		Creator = creator;
		Target = target.SteamID;
		Name = target.Name;
	}

	public DateTime RemindedAt { get; set; } = DateTime.Now;

	public DateTime CreatedAt { get; } = DateTime.Now;

	public ICommandSource Creator { get; }

	/// <summary>
	/// The steam ID targeted by the votekick
	/// </summary>
	public ulong Target { get; }

	/// <summary>
	/// The name of the player.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// List of SteamIDs which voted yes
	/// </summary>
	public List<ulong> Yes { get; } = new List<ulong>();

	/// <summary>
	/// List of SteamIDs which voted no
	/// </summary>
	public List<ulong> No { get; } = new List<ulong>();

	public bool AlreadyVoted(ulong steamId)
		=> Yes.Contains(steamId) || No.Contains(steamId);

	public void VoteYes(ulong steamId)
		=> Yes.Add(steamId);

	public void VoteNo(ulong steamId)
		=> No.Add(steamId);

}
