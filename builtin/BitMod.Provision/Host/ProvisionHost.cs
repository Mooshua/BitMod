using BitMod.Attributes.Injects;
using BitMod.Attributes.Targets;
using BitMod.Compatibility;
using BitMod.Events.Meta;
using BitMod.Events.Server;
using BitMod.Provision.Config;
using BitMod.Public;

using Lilikoi.Standard;

using Serilog;

namespace BitMod.Provision.Host;

public class ProvisionHost
{

	[Config("provisions")]
	private Dictionary<string, ProvisionServer> _servers;

	[Singleton]
	private ILogger _logger;

	[Singleton]
	private IConfigurationSystem _config;

	[BitEvent]
	public async Task OnPluginLoad(PluginLoadEvent ev)
	{
		_config.Get<Dictionary<string, ProvisionServer>>("provisions", () => new()
		{
			{ "127.0.0.1:12345", new ProvisionServer() }
		});
	}

	[BitEvent]
	public async Task OnGameServerConnected(GameServerConnectedEventArgs ev)
		=> Provision(ev.Server);

	[BitEvent]
	public async Task OnRoundEnd(RoundEndedEventArgs ev)
		=> Provision(ev.Server);


	private void Provision(BitServer server)
	{
		if (_servers.TryGetValue($"{server.GameIP.ToString()}:{server.GamePort}", out var config))
			Provision(server, config);
		else
			Provision(server, new ProvisionServer());
	}

	private void Provision(BitServer server, ProvisionServer provision)
	{
		_logger.Information("[BitMod Provision] Provisioning server {@Name} {@IP}:{@Port}", server.ServerName, server.GameIP, server.GamePort);
		_logger.Information("[BitMod Provision] Current: {@Settings}; New: {@Settings}", server.ServerSettings, provision);
		server.ServerSettings.BleedingEnabled = provision.BleedingEnabled;
		server.ServerSettings.DamageMultiplier = provision.DamageMultiplier;
		server.ServerSettings.SpectatorEnabled = provision.SpectatorsEnabled;
		server.ServerSettings.HitMarkersEnabled = provision.HitMarkersEnabled;
		server.ServerSettings.StamineEnabled = provision.StaminaEnabled;
		server.ServerSettings.FriendlyFireEnabled = provision.FriendlyFireEnabled;
		server.ServerSettings.PointLogEnabled = provision.PointLogEnabled;
		server.ServerSettings.OnlyWinnerTeamCanVote = provision.OnlyWinnersCanVote;

		if (provision.Gamemodes?.Count != 0 && provision.Gamemodes != null)
		{
			_logger.Information("[BitMod Provision] Gamemodes: {@Modes}", server.GamemodeRotation.GetGamemodeRotation().ToList());
			foreach (string gamemode in server.GamemodeRotation.GetGamemodeRotation().ToList())
				server.GamemodeRotation.RemoveFromRotation(gamemode);

			foreach (string provisionGamemode in provision.Gamemodes)
				server.GamemodeRotation.AddToRotation(provisionGamemode);
		}

		if (provision.Maps?.Count != 0 && provision.Maps != null)
		{
			_logger.Information("[BitMod Provision] Maps: {@Maps}", server.MapRotation.GetMapRotation().ToList());
			foreach (string gamemode in server.MapRotation.GetMapRotation().ToList())
				server.MapRotation.RemoveFromRotation(gamemode);

			foreach (string provisionGamemode in provision.Maps)
				server.MapRotation.AddToRotation(provisionGamemode);
		}
	}
}
