namespace BitMod.Internal;

public class BitModConfig
{
	public static BitModConfig Default()
	{
		return new BitModConfig()
		{
			Listener = ListenerConfig.Default()
		};
	}

	public ListenerConfig Listener { get; set; }
}
