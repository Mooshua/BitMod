using BitMod.Internal.Handlers;

using Serilog;

namespace BitMod.Internal.Registries;

internal class ConfigUpdatedRegistry
{
	public ConfigUpdatedRegistry(List<SimpleEventHandler> children, ILogger logger)
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
				_logger.Warning(ex, "Config update handler failed during execution!");
				if (Environment.DoNotCatchEventExceptions)
					throw;
			}
		}
	}
}
