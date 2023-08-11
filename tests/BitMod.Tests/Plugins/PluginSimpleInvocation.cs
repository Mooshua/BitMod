using BitMod.Attributes.Targets;
using BitMod.Events.Player;
using BitMod.Internal.Public;

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
		var logger = Serilog.Log.Logger;
		var context = new PluginContext( logger );
		var invoker = new PluginInvoker( context );

		context.Load("invoke_test", typeof(Host));
		invoker.Event( new PlayerDiedEventArgs() );

		Assert.Fail("Did not reach Assert.Pass located within Lilikoi container");
	}

	[Test]
	public void BlocksForInvokeToComplete()
	{
		var logger = Serilog.Log.Logger;
		var context = new PluginContext( logger );
		var invoker = new PluginInvoker( context );

		context.Load("invoke_block", typeof(DelayingHost));
		invoker.Event( new PlayerDiedEventArgs() );

		Assert.Fail("Did not reach Assert.Pass located within Lilikoi container, instead fell through without executing delay.");
	}

}
