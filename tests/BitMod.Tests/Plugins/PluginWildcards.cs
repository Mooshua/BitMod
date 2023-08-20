using System.Net;
using System.Reflection;

using BattleBitAPI.Common;
using BattleBitAPI.Server;

using BitMod.Attributes.Targets;
using BitMod.Compatibility;
using BitMod.Events.Player;
using BitMod.Tests.Helpers;

using Lilikoi.Standard;

using Serilog;

namespace BitMod.Tests.Plugins;

public class PluginWildcards : GlobalSetup
{
	public class Host
	{
		[BitEvent]
		public async Task GetRelevant(PlayerConnectedEventArgs ev, BitPlayer player, BitServer gameServer)
		{
			Assert.NotNull(player, "player != null");
			Assert.NotNull(gameServer, "gameServer != null");
			Assert.Pass("Player and GameServer wildcard successfully injected");
		}
	}


	[Test]
	public void ProducerInvokesMutator()
	{
		var mod = BitMock.Mock();
		var (player, server) = MockPlayer.New();


		mod.Context.Load("invoke_test", typeof(Host));
		mod.Invoker.Event( new PlayerConnectedEventArgs( server, player ));

		Assert.Fail("Did not reach container host");
	}
}
