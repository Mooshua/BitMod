using BitMod.Commands.Sources;
using BitMod.Compatibility;

namespace BitMod.Moderation.Votekick;

internal class VotekickComponent
{
	public VotekickComponent()
	{
		HasVotekick = false;
		CurrentVotekick = null;
	}

	public bool HasVotekick { get; private set; }

	public VotekickState? CurrentVotekick { get; private set; }

	public void Start(BitPlayer target, ICommandSource source)
	{
		if (HasVotekick)
			throw new InvalidOperationException("Cannot run two votekicks at once!");

		CurrentVotekick = new VotekickState(target, source);
		HasVotekick = true;
	}

	public void End()
	{
		CurrentVotekick = null;
		HasVotekick = false;
	}

}
