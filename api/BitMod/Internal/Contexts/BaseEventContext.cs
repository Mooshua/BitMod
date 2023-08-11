using BitMod.Internal.Handlers;
using BitMod.Internal.Registries;

using Serilog;

namespace BitMod.Internal.Contexts;

public abstract class BaseEventContext<TEvent>
{
	protected readonly Dictionary<string, List<TEvent>> _handlers = new ();

	protected ILogger _logger;

	public BaseEventContext(ILogger logger)
	{
		_logger = logger;
		Rebuild();
	}

	protected abstract void Rebuild();

	public void Remove(string pluginName)
	{
		_handlers.Remove(pluginName);
		Rebuild();
	}

	public void Add(string pluginName, IEnumerable<TEvent> handlers)
	{
		if (_handlers.ContainsKey(pluginName))
		{
			 _handlers[pluginName].AddRange(handlers);
		}
		else
		{
			_handlers.Add(pluginName, handlers.ToList());
		}

		Rebuild();
	}
}
