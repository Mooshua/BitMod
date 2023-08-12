using System.Net;

namespace BitMod.Internal;

public class ListenerConfig
{
	public static ListenerConfig Default()
	{
		return new ListenerConfig()
		{
			PublicIP = "0.0.0.0",
			Port = 9000
		};
	}

	public string PublicIP { get; set; }

	internal IPAddress GetAddress()
		=> IPAddress.Parse(PublicIP);

	public ushort Port { get; set; }
}
