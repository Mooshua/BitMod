using BitMod.Attributes.Mutators;
using BitMod.Attributes.Targets;
using BitMod.Config.Extensions;
using BitMod.Configuration.Model;
using BitMod.Events.Server;
using BitMod.Flags.Attribute;
using BitMod.Internal.Public;
using BitMod.Moderation.Events;

using Lilikoi.Standard;

using Serilog;

namespace BitMod.Moderation.Votekick;

public class VotekickController
{

	[Singleton]
	private PluginInvoker _invoker;

	[Log]
	[BitEvent]
	public async Task OnTick(GameServerTickEventArgs tick,
		ILogger logger,
		[Flag("votekick_ratio", "0.5")] IConfigSymbol requiredRatioSymbol,
		[Flag("votekick_reminder_interval", "5")] IConfigSymbol reminderIntervalSymbol,
		[Flag("votekick_duration_seconds", "25")] IConfigSymbol votekickDurationSymbol,
		[Flag("votekick_ban_duration_seconds", "3600")] IConfigSymbol banDurationSymbol,
		[Flag("votekick_min_votes", "10")] IConfigSymbol minimumVotesSymbol)
	{
		var component = tick.Server.GetComponent();
		if (!component.HasVotekick)
			return;

		var votekick = component.CurrentVotekick;
		TimeSpan durationTime = votekickDurationSymbol.AsSeconds(25, 5, 60);
		TimeSpan reminderTime = reminderIntervalSymbol.AsSeconds(5, 1, 10);
		TimeSpan banTime = banDurationSymbol.AsSeconds(3600, 0);

		long requiredVotes = minimumVotesSymbol.Int(10, 1, 32);

		double requiredRatio = requiredRatioSymbol.Float(0.5, 0, 1);
		int requiredPercent = (int)(requiredRatio * 100);

		int currentYes = votekick.Yes.Count;
		int currentNo = votekick.No.Count;
		int currentTotal = currentYes + currentNo;

		double currentRatio = currentYes / (double)currentTotal;

		int percentYes = (int)(currentRatio * 100);

		//	Is votekick over?
		if ((votekick.CreatedAt + durationTime) >= DateTime.Now)
		{
			//	Yes! CreatedAt + Duration >= Now
			OnVotekickEnd(tick, votekick, requiredRatio, currentRatio, banTime, (int)requiredVotes, currentTotal);

			component.End();
			return;
		}

		//	Is it time for another reminder?
		if ((votekick.RemindedAt + reminderTime) >= DateTime.Now)
		{
			tick.Server.SayToAllChat($"[BM] Votekick against player {votekick.Name} is ongoing.");

			votekick.RemindedAt = DateTime.Now;
		}
	}

	private void OnVotekickEnd(GameServerTickEventArgs tick, VotekickState votekick, double requiredRatio, double currentRatio, TimeSpan banTime, int minimumVotes, int totalVotes)
	{
		if (requiredRatio > currentRatio)
		{
			//	Less people voted yes than required
			tick.Server.SayToAllChat($"[BM] Player {votekick.Name} was not kicked: Too many players voted no.");
			return;
		}

		if (minimumVotes > totalVotes)
		{
			//	Less people voted at all than required
			tick.Server.SayToAllChat($"[BM] Player {votekick.Name} was not kicked: Not enough players voted!");
			return;
		}

		//	Ban the player
		tick.Server.AnnounceShort($"Player {votekick.Name} was votekicked for {(int)banTime.TotalMinutes} minutes.");
		_invoker.Event(new PlayerBannedEventArgs(votekick.Target, votekick.Name, banTime, votekick.Creator, "Votebanned"));
	}

}
