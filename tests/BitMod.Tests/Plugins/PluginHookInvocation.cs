using System.Diagnostics;

using BitMod.Attributes.Targets;
using BitMod.Events.Server;
using BitMod.Internal.Public;
using BitMod.Plugins.Events;

using Serilog;

namespace BitMod.Tests.Plugins;

public class PluginHookInvocation : GlobalSetup
{
	public class Host
	{
		[BitHook]
		public async Task<Directive> OnEvent(GameServerConnectingEventArgs ev)
		{
			Assert.NotNull(ev, "ev != null");
			await Task.Delay(100);

			return Directive.Allow;
		}
	}

	public class NeutralHost
	{
		[BitHook]
		public async Task<Directive> OnEvent(GameServerConnectingEventArgs ev)
		{
			Assert.NotNull(ev, "ev != null");

			return Directive.Neutral;
		}
	}

	public class PriorityHost
	{
		[BitHook(64)]
		public async Task<Directive> OnEvent(GameServerConnectingEventArgs ev)
		{
			Assert.NotNull(ev, "ev != null");

			return Directive.Disallow;
		}

		[BitHook(128)]
		public async Task<Directive> OnEventTwo(GameServerConnectingEventArgs ev)
		{
			Assert.NotNull(ev, "ev != null");

			return Directive.Allow;
		}
	}

	[Test]
	public void CanInvoke()
	{
		var logger = Serilog.Log.Logger;
		var context = new PluginContext( logger );
		var invoker = new PluginInvoker( context );

		context.Load("invoke_test", typeof(Host));
		var result = invoker.Hook( new GameServerConnectingEventArgs(null));

		Assert.True(result);
	}

	[Test]
	public void CanInvokeWithPriorities()
	{
		var logger = Serilog.Log.Logger;
		var context = new PluginContext( logger );
		var invoker = new PluginInvoker( context );

		context.Load("invoke_test", typeof(PriorityHost));
		var result = invoker.Hook( new GameServerConnectingEventArgs(null));

		//	Ensure that the "Disallow" hook runs before the "Allow" hook
		Assert.False(result);
	}

	[Test]
	public void CanCustomizeDefault()
	{
		var logger = Serilog.Log.Logger;
		var context = new PluginContext( logger );
		var invoker = new PluginInvoker( context );

		context.Load("invoke_test", typeof(NeutralHost));
		var result = invoker.Hook( new GameServerConnectingEventArgs(null), true);

		//	NeutralHost only returns neutral, so let our default override that.
		Assert.True(result);
	}
}
