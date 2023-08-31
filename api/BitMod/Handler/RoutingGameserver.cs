using System.Net;
using System.Threading.Tasks;

using BattleBitAPI.Common;
using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Events.Player;
using BitMod.Events.Result;
using BitMod.Events.Server;
using BitMod.Events.Squad;
using BitMod.Events.Stats;
using BitMod.Internal.Public;

namespace BitMod.Handler;

public class RoutingGameserver : BitServer
{
	private PluginInvoker _invoker;

	public RoutingGameserver(PluginInvoker invoker) : base()
	{
		_invoker = invoker;
	}

	#region Player

	public override async Task OnPlayerDied(BitPlayer player)
		=> _invoker.Event(new PlayerDiedEventArgs(this, player));

	public override async Task OnPlayerConnected(BitPlayer player)
		=> _invoker.Event(new PlayerConnectedEventArgs(this, player));

	public override async Task OnPlayerDisconnected(BitPlayer player)
		=> _invoker.Event(new PlayerDisconnectedEventArgs(this, player));

	public override async Task OnPlayerReported(BitPlayer reporter, BitPlayer target, ReportReason reason, string details)
		=> _invoker.Event(new PlayerReportedEventArgs(this, reporter, target, reason, details));

	public override async Task OnPlayerSpawned(BitPlayer player)
		=> _invoker.Event(new PlayerSpawnedEventArgs(this, player));

	public override async Task<OnPlayerSpawnArguments?> OnPlayerSpawning(BitPlayer player, OnPlayerSpawnArguments request)
		=> _invoker.Produce<PlayerSpawningEventArgs, SpawnRequest>(new PlayerSpawningEventArgs(this, player, request), new SpawnRequest(request));

	public override async Task OnAPlayerDownedAnotherPlayer(OnPlayerKillArguments<BitPlayer> args)
		=> _invoker.Event(new PlayerKilledPlayerEventArgs(this, args.Killer, args.KillerPosition, args.Victim, args.VictimPosition, args.KillerTool, args.BodyPart, args.SourceOfDamage));

	public override async Task OnPlayerChangedRole(BitPlayer player, GameRole role)
		=> _invoker.Event(new PlayerChangedRoleEventArgs(this, player, role));

	public override async Task OnPlayerChangeTeam(BitPlayer player, Team team)
		=> _invoker.Event(new PlayerChangedTeamEventArgs(this, player, team));

	public override async Task<bool> OnPlayerRequestingToChangeTeam(BitPlayer player, Team requestedTeam)
		=> _invoker.Hook(new PlayerChangingTeamEventArgs(this, player, requestedTeam), true);

	public override async Task OnPlayerGivenUp(BitPlayer player)
		=> _invoker.Event(new PlayerGivenUpEventArgs(this, player));

	public override async Task OnAPlayerRevivedAnotherPlayer(BitPlayer from, BitPlayer to)
		=> _invoker.Event(new PlayerRevivedEventArgs(this, from, to));

	public override async Task<bool> OnPlayerTypedMessage(BitPlayer sender, ChatChannel channel, string message)
		=> _invoker.Hook(new PlayerTypedMessageEventArgs(this, sender, channel, message), true);

	public override async Task<bool> OnPlayerRequestingToChangeRole(BitPlayer player, GameRole role)
		=> _invoker.Hook(new PlayerRequestingToChangeRoleEventArgs(this, player, role), true);

	#endregion

	#region Squad

	public override async Task OnSquadLeaderChanged(Squad<BitPlayer> squad, BitPlayer newLeader)
		=> _invoker.Event(new SquadLeaderChangedEventArgs(this, squad, newLeader));

	public override async Task OnPlayerJoinedSquad(BitPlayer player, Squad<BitPlayer> squad)
		=> _invoker.Event(new PlayerJoinedSquadEventArgs(this, player, squad));

	public override async Task OnPlayerLeftSquad(BitPlayer player, Squad<BitPlayer> squad)
		=> _invoker.Event(new PlayerLeftSquadEventArgs(this, player, squad));

	public override async Task OnSquadPointsChanged(Squad<BitPlayer> squad, int newPoints)
		=> _invoker.Event(new SquadPointsChangedEventArgs(this, squad, newPoints));

	#endregion

	#region Gameserver

	public override async Task OnConnected()
		=> _invoker.Event(new GameServerConnectedEventArgs(this));

	public override async Task OnDisconnected()
		=> _invoker.Event(new GameServerDisconnectedEventArgs(this));

	public override async Task OnTick()
		=> _invoker.Event(new GameServerTickEventArgs(this));

	public override async Task OnRoundStarted()
		=> _invoker.Event(new RoundStartedEventArgs(this));

	public override async Task OnRoundEnded()
		=> _invoker.Event(new RoundEndedEventArgs(this));

	public override async Task OnGameStateChanged(GameState oldState, GameState newState)
		=> _invoker.Event(new StateChangedEventArgs(this, oldState, newState));

	#endregion

	#region Stats

	public override async Task OnSavePlayerStats(ulong steamId, PlayerStats currentStats)
		=> _invoker.Event(new SavingPlayerStatsEventArgs(steamId, currentStats));

	public override async Task OnPlayerJoiningToServer(ulong steamID, PlayerJoiningArguments args)
	{
		var statsArgs = new GetPlayerStatsEventArgs(this, steamID, args.Stats);
		var matchmakingArgs = new GameServerMatchmakingEventArgs(this, steamID);
		var stats = _invoker.Produce(statsArgs, () => args.Stats);
		var matchmaking = _invoker.Produce(matchmakingArgs, () => new PlayerMatchmaking());

		args.Stats = stats;
		args.Team = matchmaking.Team;
		args.Squad = matchmaking.Squad;
	}

	#endregion

	public override Task OnSessionChanged(long oldSessionID, long newSessionID)
	{
		//	TODO: WTH is this? Implement event once documented
		return base.OnSessionChanged(oldSessionID, newSessionID);
	}
}
