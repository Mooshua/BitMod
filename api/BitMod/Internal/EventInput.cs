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

	public static EventInput From<T1, T2>(T1 eventArgs, T2 eventArgsTwo)
		where T1: class
		where T2: class
	{
		var eventInput = new EventInput();
		eventInput.Store(eventArgs);
		eventInput.Store(eventArgsTwo);

		return eventInput;
	}
}
