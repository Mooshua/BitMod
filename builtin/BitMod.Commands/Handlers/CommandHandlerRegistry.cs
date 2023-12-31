﻿using BitMod.Internal;

using Serilog;

namespace BitMod.Commands.Handlers;

internal class CommandHandlerRegistry
{
	public CommandHandlerRegistry(List<CommandHandler> children, ILogger logger)
	{
		Children = children;
		_logger = logger;
	}

	private ILogger _logger;

	public List<CommandHandler> Children { get; }

	public void Invoke(EventInput input)
	{
		foreach (CommandHandler eventHandler in Children)
		{
			try
			{
				eventHandler.Invoke(input);
			}
			catch (Exception ex)
			{
				_logger.Warning(ex, "Command handler failed during execution!");
				if (Environment.DoNotCatchEventExceptions)
					throw;
			}
		}
	}
}
