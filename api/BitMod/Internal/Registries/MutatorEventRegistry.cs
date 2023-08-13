using System;
using System.Collections.Generic;

using BitMod.Internal.Handlers;

using Serilog;

namespace BitMod.Internal.Registries;

internal class MutatorEventRegistry
{
	public MutatorEventRegistry(List<MutatorEventHandler> children, ILogger logger)
	{
		Children = children;
		_logger = logger;
	}

	private ILogger _logger;

	public List<MutatorEventHandler> Children { get; }

	public void Invoke(EventInput input)
	{
		foreach (MutatorEventHandler eventHandler in Children)
		{
			try
			{
				eventHandler.Invoke(input);
			}
			catch (Exception ex)
			{
				_logger.Warning(ex, "Mutator failed during execution!");
				if (Environment.DoNotCatchEventExceptions)
					throw;
			}
		}
	}
}
