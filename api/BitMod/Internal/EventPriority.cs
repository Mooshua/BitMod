namespace BitMod.Internal;

public class EventPriority
{
	public EventPriority(byte priority)
	{
		Priority = priority;
	}

	public byte Priority { get; }
}
