using System.Net;

using BitMod.Compatibility;
using BitMod.Events.Player;
using BitMod.Events.Result;
using BitMod.Events.Server;
using BitMod.Events.Stats;
using BitMod.Internal.Public;

using CommunityServerAPI.BattleBitAPI.Common.Arguments;
using CommunityServerAPI.BattleBitAPI.Common.Data;
using CommunityServerAPI.BattleBitAPI.Common.Enums;
using CommunityServerAPI.BattleBitAPI.Server;

namespace BitMod.Handler;

public class RoutingHandler : DefaultHandler<BitPlayer>
{
	private PluginInvoker _invoker;

	public RoutingHandler(ServerListener<BitPlayer> server, PluginInvoker invoker) : base(server)
	{
		_invoker = invoker;
	}

	#region Player

	public override async Task OnPlayerDied(BitPlayer player)
		=> _invoker.Event(new PlayerDiedEventArgs(player));

	public override async Task OnPlayerConnected(BitPlayer player)
		=> _invoker.Event(new PlayerConnectedEventArgs(player));

	public override async Task OnPlayerDisconnected(BitPlayer player)
		=> _invoker.Event(new PlayerDisconnectedEventArgs(player));

	public override async Task OnPlayerReported(BitPlayer reporter, BitPlayer target, ReportReason reason, string details)
		=> _invoker.Event(new PlayerReportedEventArgs(reporter, target, reason, details));

	public override async Task OnPlayerSpawned(BitPlayer player)
		=> _invoker.Event(new PlayerSpawnedEventArgs(player));

	public override async Task<PlayerSpawnRequest> OnPlayerSpawning(BitPlayer player, PlayerSpawnRequest request)
		=> _invoker.Produce<PlayerSpawningEventArgs, SpawnRequest>(new PlayerSpawningEventArgs(player, request), new SpawnRequest(request));

	public override async Task OnAPlayerKilledAnotherPlayer(OnPlayerKillArguments<BitPlayer> args)
		=> _invoker.Event(new PlayerKilledPlayerEventArgs(args.Killer, args.KillerPosition, args.Victim, args.VictimPosition, args.KillerTool, args.BodyPart, args.SourceOfDamage));

	public override async Task OnPlayerChangedRole(BitPlayer player, GameRole role)
		=> _invoker.Event(new PlayerChangedRoleEventArgs(player, role));

	public override async Task OnPlayerChangedTeam(BitPlayer player, Team team)
		=> _invoker.Event(new PlayerChangedTeamEventArgs(player, team));

	public override async Task OnPlayerLeftSquad(BitPlayer player, Squads left)
		=> _invoker.Event(new PlayerLeftSquadEventArgs(player, left));

	public override async Task OnPlayerJoinedASquad(BitPlayer player, Squads joined)
		=> _invoker.Event(new PlayerJoinedSquadEventArgs(player, joined));

	public override async Task<bool> OnPlayerTypedMessage(BitPlayer sender, ChatChannel channel, string message)
		=> _invoker.Hook(new PlayerTypedMessageEventArgs(sender, channel, message), true);

	public override async Task<bool> OnPlayerRequestingToChangeRole(BitPlayer player, GameRole role)
		=> _invoker.Hook(new PlayerRequestingToChangeRoleEventArgs(player, role), true);

	#endregion

	#region Gameserver

	public override async Task<bool> OnGameServerConnecting(IPAddress source)
		=> _invoker.Hook(new GameServerConnectingEventArgs(source), false);

	public override async Task OnGameServerConnected(GameServer server)
		=> _invoker.Event(new GameServerConnectedEventArgs(server));

	public override async Task OnGameServerDisconnected(GameServer server)
		=> _invoker.Event(new GameServerDisconnectedEventArgs(server));

	public override async Task OnGameServerReconnected(GameServer server)
		=> _invoker.Event(new GameServerReconnectedEventArgs(server));

	public override async Task OnGameServerTick(GameServer server)
		=> _invoker.Event(new GameServerTickEventArgs(server));

	#endregion

	#region Stats

	public override async Task OnSavePlayerStats(ulong steamId, PlayerStats currentStats)
		=> _invoker.Event(new SavingPlayerStatsEventArgs(steamId, currentStats));

	public override async Task<PlayerStats> OnGetPlayerStats(ulong steamId, PlayerStats fromOfficialServers)
		=> _invoker.Produce(new GetPlayerStatsEventArgs(steamId, fromOfficialServers), fromOfficialServers);

	#endregion

}
