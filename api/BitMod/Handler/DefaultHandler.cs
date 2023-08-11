using System.Net;

using CommunityServerAPI.BattleBitAPI;
using CommunityServerAPI.BattleBitAPI.Common.Arguments;
using CommunityServerAPI.BattleBitAPI.Common.Data;
using CommunityServerAPI.BattleBitAPI.Common.Enums;
using CommunityServerAPI.BattleBitAPI.Server;

namespace BitMod.Handler;

public class DefaultHandler<TPlayer> : BaseHandler<TPlayer> where TPlayer : Player
{
	public DefaultHandler(ServerListener<TPlayer> server) : base(server)
	{
	}

	/// <inheritdoc />
	public override Task OnGameServerConnected(GameServer server)
		=> Task.CompletedTask;

	/// <inheritdoc />
	public override Task OnGameServerReconnected(GameServer server)
		=> Task.CompletedTask;

	/// <inheritdoc />
	public override Task OnGameServerDisconnected(GameServer server)
		=> Task.CompletedTask;

	/// <inheritdoc />
	public override Task OnPlayerConnected(TPlayer player)
		=> Task.CompletedTask;

	/// <inheritdoc />
	public override Task OnPlayerDisconnected(TPlayer player)
		=> Task.CompletedTask;

	/// <inheritdoc />
	public override Task<bool> OnPlayerTypedMessage(TPlayer sender, ChatChannel channel, string message)
		=> Task.FromResult(true);

	/// <inheritdoc />
	public override Task OnAPlayerKilledAnotherPlayer(OnPlayerKillArguments<TPlayer> args)
		=> Task.CompletedTask;

	/// <inheritdoc />
	public override Task<PlayerStats> OnGetPlayerStats(ulong steamId, PlayerStats fromOfficialServers)
		=> Task.FromResult(fromOfficialServers);

	/// <inheritdoc />
	public override Task OnSavePlayerStats(ulong steamId, PlayerStats currentStats)
		=> Task.CompletedTask;

	/// <inheritdoc />
	public override Task<bool> OnPlayerRequestingToChangeRole(TPlayer player, GameRole role)
		=> Task.FromResult(true);

	/// <inheritdoc />
	public override Task OnPlayerChangedRole(TPlayer player, GameRole role)
		=> Task.CompletedTask;

	/// <inheritdoc />
	public override Task OnPlayerJoinedASquad(TPlayer player, Squads joined)
		=> Task.CompletedTask;

	/// <inheritdoc />
	public override Task OnPlayerLeftSquad(TPlayer player, Squads left)
		=> Task.CompletedTask;

	/// <inheritdoc />
	public override Task OnPlayerChangedTeam(TPlayer player, Team team)
		=> Task.CompletedTask;

	/// <inheritdoc />
	public override Task<PlayerSpawnRequest> OnPlayerSpawning(TPlayer player, PlayerSpawnRequest request)
		=> Task.FromResult(request);

	/// <inheritdoc />
	public override Task OnPlayerSpawned(TPlayer player)
		=> Task.CompletedTask;

	/// <inheritdoc />
	public override Task OnGameServerTick(GameServer server)
		=> Task.CompletedTask;

	/// <inheritdoc />
	public override Task<bool> OnGameServerConnecting(IPAddress source)
		=> Task.FromResult(true);

	/// <inheritdoc />
	public override Task OnPlayerDied(TPlayer player)
		=> Task.CompletedTask;

	/// <inheritdoc />
	public override Task OnPlayerReported(TPlayer reporter, TPlayer target, ReportReason reason, string details)
		=> Task.CompletedTask;
}
