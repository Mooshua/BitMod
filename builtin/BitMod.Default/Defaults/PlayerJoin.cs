using BitMod.Attributes.Targets;
using BitMod.Events.Stats;
using BitMod.Plugins.Events;

namespace BitMod.Default.Defaults;

public class PlayerJoin
{

	[BitProducer(Priority.LAST)]
	public Task<Product> GetStats(GetPlayerStatsEventArgs ev)
	{

		return Task.FromResult(Product.Produce(ev.OfficialStats));
	}

}
