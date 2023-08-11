using System.Net;

namespace BitMod.Internal;

public class ListenerConfig
{
	public IPAddress Address { get; set; } = IPAddress.Any;

	public ushort Port { get; set; } = 29999;
}
