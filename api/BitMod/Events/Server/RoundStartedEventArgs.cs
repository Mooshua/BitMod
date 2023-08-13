using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Server;

public class RoundStartedEventArgs : IEventArgs, IGameserverEvent
{
	public RoundStartedEventArgs(BitServer server)
	{
		Server = server;
	}

	public BitServer Server { get; }
}
