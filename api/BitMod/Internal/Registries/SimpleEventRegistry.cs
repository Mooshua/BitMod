using System;
using System.Collections.Generic;

using BitMod.Internal.Handlers;

using Serilog;

namespace BitMod.Internal.Registries;

internal class SimpleEventRegistry
{
	public SimpleEventRegistry(List<SimpleEventHandler> children, ILogger logger)
	{
		Children = children;
		_logger = logger;
	}

	private ILogger _logger;

	public List<SimpleEventHandler> Children { get; }

	public void Invoke(EventInput input)
	{
		foreach (SimpleEventHandler eventHandler in Children)
		{
			try
			{
				eventHandler.Invoke(input);
			}
			catch (Exception ex)
			{
				_logger.Warning(ex, "Event handler for {@Type} failed during execution!", input.Type?.FullName);
				if (Environment.DoNotCatchEventExceptions)
					throw;
			}
		}
	}
}
