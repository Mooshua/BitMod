#region

using System.Net;

using CommunityServerAPI.BattleBitAPI;
using CommunityServerAPI.BattleBitAPI.Common.Arguments;
using CommunityServerAPI.BattleBitAPI.Common.Data;
using CommunityServerAPI.BattleBitAPI.Common.Enums;
using CommunityServerAPI.BattleBitAPI.Server;

#endregion

namespace BitMod.Handler;

public abstract class BaseHandler<TPlayer> : IDisposable
	where TPlayer : Player
{
	protected ServerListener<TPlayer> _server;

	public BaseHandler(ServerListener<TPlayer> server)
	{
		_server = server;

		_server.OnGameServerConnected += OnGameServerConnected;
		_server.OnGameServerDisconnected += OnGameServerDisconnected;
		_server.OnGameServerTick += OnGameServerTick;
		_server.OnGameServerReconnected += OnGameServerReconnected;
		_server.OnGameServerConnecting += OnGameServerConnecting;

		_server.OnPlayerConnected += OnPlayerConnected;
		_server.OnPlayerDied += OnPlayerDied;
		_server.OnPlayerDisconnected += OnPlayerDisconnected;
		_server.OnPlayerReported += OnPlayerReported;
		_server.OnPlayerSpawned += OnPlayerSpawned;
		_server.OnPlayerSpawning += OnPlayerSpawning;
		_server.OnPlayerChangedRole += OnPlayerChangedRole;
		_server.OnPlayerChangedTeam += OnPlayerChangedTeam;
		_server.OnPlayerLeftSquad += OnPlayerLeftSquad;
		_server.OnPlayerTypedMessage += OnPlayerTypedMessage;
		_server.OnPlayerJoinedASquad += OnPlayerJoinedASquad;
		_server.OnPlayerRequestingToChangeRole += OnPlayerRequestingToChangeRole;

		_server.OnAPlayerKilledAnotherPlayer += OnAPlayerKilledAnotherPlayer;

		_server.OnGetPlayerStats += OnGetPlayerStats;
		_server.OnSavePlayerStats += OnSavePlayerStats;
	}

	public void Dispose()
	{
		_server.OnGameServerConnected -= OnGameServerConnected;
		_server.OnGameServerDisconnected -= OnGameServerDisconnected;
		_server.OnGameServerTick -= OnGameServerTick;
		_server.OnGameServerReconnected -= OnGameServerReconnected;
		_server.OnGameServerConnecting -= OnGameServerConnecting;

		_server.OnPlayerConnected -= OnPlayerConnected;
		_server.OnPlayerDied -= OnPlayerDied;
		_server.OnPlayerDisconnected -= OnPlayerDisconnected;
		_server.OnPlayerReported -= OnPlayerReported;
		_server.OnPlayerSpawned -= OnPlayerSpawned;
		_server.OnPlayerSpawning -= OnPlayerSpawning;
		_server.OnPlayerChangedRole -= OnPlayerChangedRole;
		_server.OnPlayerChangedTeam -= OnPlayerChangedTeam;
		_server.OnPlayerLeftSquad -= OnPlayerLeftSquad;
		_server.OnPlayerTypedMessage -= OnPlayerTypedMessage;
		_server.OnPlayerJoinedASquad -= OnPlayerJoinedASquad;
		_server.OnPlayerRequestingToChangeRole -= OnPlayerRequestingToChangeRole;

		_server.OnAPlayerKilledAnotherPlayer -= OnAPlayerKilledAnotherPlayer;

		_server.OnGetPlayerStats -= OnGetPlayerStats;
		_server.OnSavePlayerStats -= OnSavePlayerStats;
	}

	/// <summary>
	///     Fired when a game server connects.
	/// </summary>
	/// <remarks>
	///     GameServer: Game server that is connecting.<br />
	/// </remarks>
	public abstract Task OnGameServerConnected(GameServer server);

	/// <summary>
	///     Fired when a game server reconnects. (When game server connects while a socket is already open)
	/// </summary>
	/// <remarks>
	///     GameServer: Game server that is reconnecting.<br />
	/// </remarks>
	public abstract Task OnGameServerReconnected(GameServer server);

	/// <summary>
	///     Fired when a game server disconnects. Check (GameServer.TerminationReason) to see the reason.
	/// </summary>
	/// <remarks>
	///     GameServer: Game server that disconnected.<br />
	/// </remarks>
	public abstract Task OnGameServerDisconnected(GameServer server);

	/// <summary>
	///     Fired when a player connects to a server.<br />
	///     Check player.GameServer get the server that player joined.
	/// </summary>
	/// <remarks>
	///     Player: The player that connected to the server<br />
	/// </remarks>
	public abstract Task OnPlayerConnected(TPlayer player);

	/// <summary>
	///     Fired when a player disconnects from a server.<br />
	///     Check player.GameServer get the server that player left.
	/// </summary>
	/// <remarks>
	///     Player: The player that disconnected from the server<br />
	/// </remarks>
	public abstract Task OnPlayerDisconnected(TPlayer player);

	/// <summary>
	///     Fired when a player types a message to text chat.<br />
	/// </summary>
	/// <remarks>
	///     Player: The player that typed the message <br />
	///     ChatChannel: The channel the message was sent <br />
	///     string - Message: The message<br />
	/// </remarks>
	/// <value>
	///     Returns: True if you let the message broadcasted, false if you don't it to be broadcasted.
	/// </value>
	public abstract Task<bool> OnPlayerTypedMessage(TPlayer sender, ChatChannel channel, string message);

	/// <summary>
	///     Fired when a player kills another player.
	/// </summary>
	/// <remarks>
	///     OnPlayerKillArguments: Details about the kill<br />
	/// </remarks>
	public abstract Task OnAPlayerKilledAnotherPlayer(OnPlayerKillArguments<TPlayer> args);

	/// <summary>
	///     Fired when game server requests the stats of a player, this function should return in 3000ms or player will not
	///     able to join to server.
	/// </summary>
	/// <remarks>
	///     ulong - SteamID of the player<br />
	///     PlayerStats - The official stats of the player<br />
	/// </remarks>
	/// <value>
	///     Returns: The modified stats of the player.
	/// </value>
	public abstract Task<PlayerStats> OnGetPlayerStats(ulong steamId, PlayerStats fromOfficialServers);

	/// <summary>
	///     Fired when game server requests to save the stats of a player.
	/// </summary>
	/// <remarks>
	///     ulong - SteamID of the player<br />
	///     PlayerStats - Stats of the player<br />
	/// </remarks>
	/// <value>
	///     Returns: The stats of the player.
	/// </value>
	public abstract Task OnSavePlayerStats(ulong steamId, PlayerStats currentStats);

	/// <summary>
	///     Fired when a player requests server to change role.
	/// </summary>
	/// <remarks>
	///     TPlayer - The player requesting<br />
	///     GameRole - The role the player asking to change<br />
	/// </remarks>
	/// <value>
	///     Returns: True if you accept if, false if you don't.
	/// </value>
	public abstract Task<bool> OnPlayerRequestingToChangeRole(TPlayer player, GameRole role);

	/// <summary>
	///     Fired when a player changes their game role.
	/// </summary>
	/// <remarks>
	///     TPlayer - The player<br />
	///     GameRole - The new role of the player<br />
	/// </remarks>
	public abstract Task OnPlayerChangedRole(TPlayer player, GameRole role);

	/// <summary>
	///     Fired when a player joins a squad.
	/// </summary>
	/// <remarks>
	///     TPlayer - The player<br />
	///     Squads - The squad player joined<br />
	/// </remarks>
	public abstract Task OnPlayerJoinedASquad(TPlayer player, Squads joined);

	/// <summary>
	///     Fired when a player leaves their squad.
	/// </summary>
	/// <remarks>
	///     TPlayer - The player<br />
	///     Squads - The squad that player left<br />
	/// </remarks>
	public abstract Task OnPlayerLeftSquad(TPlayer player, Squads left);

	/// <summary>
	///     Fired when a player changes team.
	/// </summary>
	/// <remarks>
	///     TPlayer - The player<br />
	///     Team - The new team that player joined<br />
	/// </remarks>
	public abstract Task OnPlayerChangedTeam(TPlayer player, Team team);

	/// <summary>
	///     Fired when a player is spawning.
	/// </summary>
	/// <remarks>
	///     TPlayer - The player<br />
	///     PlayerSpawnRequest - The request<br />
	/// </remarks>
	/// <value>
	///     Returns: The new spawn response
	/// </value>
	public abstract Task<PlayerSpawnRequest> OnPlayerSpawning(TPlayer player, PlayerSpawnRequest request);

	/// <summary>
	///     Fired when a player is spawns
	/// </summary>
	/// <remarks>
	///     TPlayer - The player<br />
	/// </remarks>
	public abstract Task OnPlayerSpawned(TPlayer player);

	/// <summary>
	///     Fired when game server is ticking (~100hz)<br />
	/// </summary>
	public abstract Task OnGameServerTick(GameServer server);

	/// <summary>
	///     Fired when an attempt made to connect to the server.<br />
	///     Default, any connection attempt will be accepted
	/// </summary>
	/// <remarks>
	///     IPAddress: IP of incoming connection <br />
	/// </remarks>
	/// <value>
	///     Returns: true if allow connection, false if deny the connection.
	/// </value>
	public abstract Task<bool> OnGameServerConnecting(IPAddress source);

	/// <summary>
	///     Fired when a player dies
	/// </summary>
	/// <remarks>
	///     TPlayer - The player<br />
	/// </remarks>
	public abstract Task OnPlayerDied(TPlayer player);

	/// <summary>
	///     Fired when a player reports another player.
	/// </summary>
	/// <remarks>
	///     TPlayer - The reporter player<br />
	///     TPlayer - The reported player<br />
	///     ReportReason - The reason of report<br />
	///     String - Additional detail<br />
	/// </remarks>
	public abstract Task OnPlayerReported(TPlayer reporter, TPlayer target, ReportReason reason, string details);
}
