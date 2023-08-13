using System.Net;

using BattleBitAPI.Common;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

using BitMod.Attributes.Targets;
using BitMod.Events.Player;
using BitMod.Events.Server;
using BitMod.Internal.Public;
using BitMod.Plugins.Events;

namespace BitMod.Benchmarks.Events;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser(true)]
public class HookChain
{

	public static int Blackbox = 0;

	public class Host
	{

		[BitHook]
		public async Task<Directive> MyEvent(GameServerConnectingEventArgs ev)
		{
			Blackbox++;

			return Directive.Neutral;
		}

	}

	[Params(1, 5, 10)]
	public int ChainLength;

	private PluginContext _context;
	private PluginInvoker _invoker;

	[GlobalSetup]
	public void SetUp()
	{
		var mock = BitMock.Mock();
		_context = mock.Context;
		_invoker = mock.Invoker;

		for (int i = 0; i < ChainLength; i++)
			_context.Load("bench", typeof(Host));
	}

	[Benchmark]
	public bool InvokeHook()
		=> _invoker.Hook(new GameServerConnectingEventArgs( IPAddress.Any) );

}
