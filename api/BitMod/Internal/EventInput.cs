using Lilikoi.Context;

namespace BitMod.Internal;

internal class EventInput : Mount
{

	public static EventInput From<T>(T eventArgs)
		where T: class
	{
		var eventInput = new EventInput();
		eventInput.Store(eventArgs);

		return eventInput;
	}
}
