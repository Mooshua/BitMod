using BattleBitAPI.Common;
using BattleBitAPI.Server;

using BitMod.Attributes.Injects;
using BitMod.Attributes.Targets;
using BitMod.Compatibility;
using BitMod.Configuration.Model;
using BitMod.Events.Meta;
using BitMod.Events.Server;
using BitMod.Provision.Config;
using BitMod.Provision.Helpers;
using BitMod.Public;

using Lilikoi.Standard;

using Serilog;

namespace BitMod.Provision.Host;

public class ProvisionHost
{
	[Config("provisions")]
	private IConfigObject _config;

	[Singleton]
	private ILogger _logger;


	[BitEvent]
	public async Task OnServerConnected(GameServerConnectedEventArgs ev)
	{
		Provision(ev.Server);
	}

	[BitEvent]
	public async Task OnServerRoundEnd(RoundEndedEventArgs ev)
	{
		Provision(ev.Server);
	}

	[BitEvent]
    	public async Task OnPlayerConnected(PlayerConnectedEventArgs ev)
    	{
       		Provision(ev.Server);
	}

	private void Provision(BitServer server)
	{
		var globalAdapter = new ProvisionConfigAdapter(_config);
		var localAdapter = globalAdapter.GetServer(server);

		_logger.Information("Provision for for server {@Server}: {@Local}", server.ToString(), localAdapter);

		if (localAdapter == null)
			return;

		if (localAdapter.HasMapcycle())
		{
			_logger.Debug("Provisioning {@Server}'s mapcycle to {@Mapcycle}", server.ToString(), localAdapter.GetMapcycle());
			MapcycleValidator.ValidateMaps(_logger, localAdapter.GetMapcycle());
			server.MapRotation.SetRotation(localAdapter.GetMapcycle());
		}

		if (localAdapter.HasGamemodes())
		{
			_logger.Debug("Provisioning {@Server}'s gamemodes to {@Gamemodes}", server.ToString(), localAdapter.GetGamemodes());
			GamemodeValidator.ValidateGamemodes(_logger, localAdapter.GetGamemodes());
			server.GamemodeRotation.SetRotation(localAdapter.GetGamemodes());
		}

		if (localAdapter.HasLoadingText())
		{
			_logger.Debug("Provisioning {@Server}'s loading text to {@Text}", server.ToString(), localAdapter.GetLoadingText());
			server.SetLoadingScreenText(localAdapter.GetLoadingText());
		}

		if (localAdapter.HasMinPlayers())
		{
			_logger.Debug("Provisioning {@Server}'s min players to {@MinPlayers}", server.ToString(), localAdapter.GetMinPlayers());
			server.RoundSettings.PlayersToStart = (int) localAdapter.GetMinPlayers();
		}
	}
}
