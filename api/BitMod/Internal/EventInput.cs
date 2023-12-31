﻿using System;

using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;
using BitMod.Logging;

using Lilikoi.Context;

using Serilog.Core;

namespace BitMod.Internal;

public class EventInput : Mount, IResponsiblePlayerEvent, IGameserverEvent, ILogMetadataSource
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

	public BitServer Server { get; private set; }

	private void Write<T>(T eventArgs)
	{
		if (typeof(IResponsiblePlayerEvent).IsAssignableFrom(typeof(T)))
			ResponsiblePlayer = (eventArgs as IResponsiblePlayerEvent).ResponsiblePlayer;
		if (typeof(IGameserverEvent).IsAssignableFrom(typeof(T)))
			Server = (eventArgs as IGameserverEvent).Server;
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

	public IEnumerable<ILogEventEnricher> GetMetadata()
		=> new ILogEventEnricher[0];
}
