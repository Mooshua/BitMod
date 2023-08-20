using BitMod.Attributes.Injects;
using BitMod.Commands.Attributes;
using BitMod.Commands.Attributes.Parameters;
using BitMod.Commands.Handlers;
using BitMod.Commands.Sources;
using BitMod.Compatibility;

namespace BitMod.Moderation.Votekick;

public class VotekickCommands
{

	private bool IsValid(ICommandSource source, IBitObject server, out VotekickState state)
	{
		state = null;
		//	Ensure there is an active votekick.
		if (!server.Has<VotekickState>())
		{
			source.Reply("[BitMod] No active votekick!");
			return false;
		}

		var voteState = server.Get<VotekickState>();
		if (voteState.AlreadyVoted(source.Steam64))
		{
			source.Reply("[BitMod] You already voted!");
			return false;
		}

		state = voteState;
		return true;
	}

	[BitCommand("yes", "Vote yes to the current votekick")]
	[BitCommandRequire(InGame = true, IsLoggedIn = true)]
	public async Task OnVoteYes([CommandSender] ICommandSource source, [RelevantGameserver] BitServer server)
	{
		var bitObject = server as IBitObject;

		if (IsValid(source, bitObject, out var state))
		{
			state.VoteYes(source.Steam64);
			source.Reply("[BitMod] Voted yes to votekick");
		}
	}

	[BitCommand("no", "Vote no to the current votekick")]
	[BitCommandRequire(InGame = true, IsLoggedIn = true)]
	public async Task OnVoteNo([CommandSender] ICommandSource source, [RelevantGameserver] BitServer server)
	{
		var bitObject = server as IBitObject;

		if (IsValid(source, bitObject, out var state))
		{
			state.VoteNo(source.Steam64);
			source.Reply("[BitMod] Voted no to votekick");
		}
	}

	[BitCommand("votekick", "")]
	public async Task OnStartVotekick(
		[CommandSender] ICommandSource source,
		[RelevantGameserver] BitServer server,
		[CommandArg(0)] CommandArg target)
	{

	}

}
