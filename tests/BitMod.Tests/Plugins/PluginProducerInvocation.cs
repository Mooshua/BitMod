using BitMod.Attributes.Targets;
using BitMod.Events.Server;
using BitMod.Events.Stats;
using BitMod.Internal.Public;
using BitMod.Plugins.Events;

using CommunityServerAPI.BattleBitAPI.Common.Data;

namespace BitMod.Tests.Plugins;

public class PluginProducerInvocation : GlobalSetup
{
	public class Host
	{

		[BitMutator]
		public async Task Mutator(GetPlayerStatsEventArgs ev, PlayerStats stats)
		{
			Assert.NotNull(ev, "Mutator ev != null");
			Assert.NotNull(stats, "Mutator stats != null");

			Assert.Pass("I'm the mutator!");
		}

		[BitProducer]
		public async Task<Product> Producer(GetPlayerStatsEventArgs ev)
		{
			Assert.NotNull(ev, "Producer ev != null");

			return Product.Produce( new PlayerStats() );
		}

	}

	[Test]
	public void ProducerInvokesMutator()
	{
		var logger = Serilog.Log.Logger;
		var context = new PluginContext( logger );
		var invoker = new PluginInvoker( context );

		context.Load("invoke_test", typeof(Host));
		var result = invoker.Produce<GetPlayerStatsEventArgs, PlayerStats>( new GetPlayerStatsEventArgs(default, null), () => null);

		Assert.Fail("Did not reach mutator block");
	}
}
