using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

using BitMod.Attributes.Targets;
using BitMod.Events.Player;
using BitMod.Internal.Public;

namespace BitMod.Benchmarks.Events;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser(true)]
public class EventChain
{

	public static int Blackbox = 0;

	public class Host
	{

		[BitEvent]
		public async Task MyEvent(PlayerConnectedEventArgs ev)
		{
			Blackbox++;
		}

	}

	[Params(0, 5, 10)]
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
	public void InvokeEvent()
		=> _invoker.Event(new PlayerConnectedEventArgs(null));

}
