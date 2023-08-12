using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

using Lilikoi.Context;

namespace BitMod.Internal;

public class EventInput : Mount, IResponsiblePlayerAccessor, IRelevantGameserverAccessor
{
	public EventInput(IBaseArgs args, Type type)
	{
		Args = args;
		Type = type;
	}

	public IBaseArgs Args { get; }

	/// <summary>
	/// The event arg input being routed
	/// </summary>
	public Type Type { get; }

	public BitPlayer? ResponsiblePlayer { get; private set; }

	public GameServer? RelevantGameServer { get; private set; }

	private void Write<T>(T eventArgs)
	{
		if (typeof(IResponsiblePlayerAccessor).IsAssignableFrom(typeof(T)))
			ResponsiblePlayer = (eventArgs as IResponsiblePlayerAccessor).ResponsiblePlayer;
		if (typeof(IRelevantGameserverAccessor).IsAssignableFrom(typeof(T)))
			RelevantGameServer = (eventArgs as IRelevantGameserverAccessor).RelevantGameserver;
	}

	public static EventInput From<T>(T eventArgs)
		where T: class, IBaseArgs
	{
		var eventInput = new EventInput(eventArgs, typeof(T));
		eventInput.Write(eventArgs);
		eventInput.Store(eventArgs);

		return eventInput;
	}

	public static EventInput From<T1, T2>(T1 eventArgs, T2 eventArgsTwo)
		where T1: class, IBaseArgs
		where T2: class
	{
		var eventInput = new EventInput(eventArgs, typeof(T1));
		eventInput.Write(eventArgs);
		eventInput.Store(eventArgs);

		eventInput.Store(eventArgsTwo);

		return eventInput;
	}
}
