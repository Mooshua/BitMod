using BitMod.Commands.Sources;
using BitMod.Compatibility;

namespace BitMod.Moderation.Votekick;

public static class VotekickExtensions
{
	internal static VotekickComponent GetComponent(this BitServer server)
	{
		if (!server.Mount.Has<VotekickComponent>())
			server.Mount.Store(new VotekickComponent());

		return server.Mount.Get<VotekickComponent>()!;
	}

	public static VotekickState? GetVotekick(this BitServer server)
		=> server.GetComponent().CurrentVotekick;

	public static bool HasVotekick(this BitServer server)
		=> server.GetComponent().HasVotekick;

	public static void StartVotekick(this BitServer server, BitPlayer target, ICommandSource source)
		=> server.GetComponent().Start(target, source);

	public static void EndVotekick(this BitServer server)
		=> server.GetComponent().End();

}
