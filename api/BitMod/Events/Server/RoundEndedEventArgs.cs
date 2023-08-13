using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Server;

public class RoundEndedEventArgs : IEventArgs, IGameserverEvent
{
	public RoundEndedEventArgs(BitServer server)
	{
		Server = server;
	}

	public BitServer Server { get; }
}
