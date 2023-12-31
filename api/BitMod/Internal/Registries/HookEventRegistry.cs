﻿using System;
using System.Collections.Generic;
using System.Linq;

using BitMod.Internal.Handlers;
using BitMod.Plugins.Events;

using Serilog;

namespace BitMod.Internal.Registries;

internal class HookEventRegistry
{
	public HookEventRegistry(List<HookEventHandler> children, ILogger logger)
	{
		Children = children
			.OrderByDescending(ev => ev.Priority)
			.ToList();
		_logger = logger;
	}

	private ILogger _logger;

	public List<HookEventHandler> Children { get; }

	public Directive Invoke(EventInput input)
	{
		foreach (HookEventHandler hookEvent in Children)
		{
			try
			{
				var hookResult = hookEvent.Invoke(input);

				if (hookResult != Directive.Neutral)
					return hookResult;
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Hook failed during execution!");
				if (Environment.DoNotCatchEventExceptions)
					throw;
			}
		}

		//	Entire chain is completely neutral.
		return Directive.Neutral;
	}
}
