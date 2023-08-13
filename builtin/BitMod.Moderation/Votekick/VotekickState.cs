namespace BitMod.Moderation.Votekick;

public class VotekickState
{

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
