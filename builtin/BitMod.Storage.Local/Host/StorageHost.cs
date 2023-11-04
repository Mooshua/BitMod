
using BattleBitAPI.Common;

using BitMod.Attributes.Injects;
using BitMod.Attributes.Targets;
using BitMod.Configuration.Model;
using BitMod.Events.Stats;
using BitMod.Plugins.Events;
using BitMod.Storage.Local.Config;

namespace BitMod.Storage.Local.Host;

public class StorageHost
{

	[Config("storage_local")]
	private IConfigObject _configObject;

	private StorageConfigAdapter _config => new StorageConfigAdapter(_configObject);

	private string Build(string id)
	{
		if (!_config.HasPath() || _config.Path == null)
			throw new InvalidOperationException("local storage config does not have a valid path!");

		Directory.CreateDirectory(_config.Path);
		return Path.Combine(_config.Path, id);
	}

	[BitProducer]
	public async Task<Product> OnGetStats(GetPlayerStatsEventArgs ev)
	{
		var path = Build(ev.SteamID.ToString());

		if (!File.Exists(path))
			return Product.None();

		var bytes = File.ReadAllBytes(path);

		return Product.Produce(new PlayerStats(bytes));
	}

	[BitEvent]
	public async Task OnSaveStats(SavingPlayerStatsEventArgs ev)
	{
		var path = Build(ev.SteamID.ToString());

		File.WriteAllBytes(path, ev.PlayerStats.SerializeToByteArray());
	}

}
