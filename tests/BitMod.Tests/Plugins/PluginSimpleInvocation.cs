using BitMod.Attributes.Targets;
using BitMod.Events.Player;
using BitMod.Internal.Public;
using BitMod.Tests.Helpers;

namespace BitMod.Tests.Plugins;

public class PluginSimpleInvocation : GlobalSetup
{

	public class Host
	{
		[BitEvent]
		public Task OnEvent(PlayerDiedEventArgs ev)
		{
			Assert.Pass();

			return Task.CompletedTask;
		}
	}

	public class DelayingHost
	{
		[BitEvent]
		public async Task OnEvent(PlayerDiedEventArgs ev)
		{
			await Task.Delay(1000);

			Assert.Pass();
		}
	}

	[Test]
	public void CanInvoke()
	{
		var mod = BitMock.Mock();
		var (player, server) = MockPlayer.New();

		mod.Context.Load("invoke_test", typeof(Host));
		mod.Invoker.Event( new PlayerDiedEventArgs( server, player ) );

		Assert.Fail("Did not reach Assert.Pass located within Lilikoi container");
	}

	[Test]
	public void BlocksForInvokeToComplete()
	{
		var mod = BitMock.Mock();
		var (player, server) = MockPlayer.New();

		mod.Context.Load("invoke_block", typeof(DelayingHost));
		mod.Invoker.Event( new PlayerDiedEventArgs( server, player ) );

		Assert.Fail("Did not reach Assert.Pass located within Lilikoi container, instead fell through without executing delay.");
	}

}
