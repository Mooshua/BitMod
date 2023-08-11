using BitMod.Internal.Handlers;
using BitMod.Internal.Registries;

namespace BitMod.Internal.Contexts;

internal class SimpleEventContext
{
	private readonly Dictionary<string, List<SimpleEventHandler>> _handlers = new Dictionary<string, List<SimpleEventHandler>>();

	private SimpleEventRegistry _currentRegistry;

	public SimpleEventContext()
	{
		_currentRegistry = new SimpleEventRegistry(new List<SimpleEventHandler>());
	}

	private void Rebuild()
	{
		_currentRegistry = new SimpleEventRegistry(_handlers
			.SelectMany(kv => kv.Value)
			.ToList());
	}

	public void Remove(string pluginName)
	{
		_handlers.Remove(pluginName);
		Rebuild();
	}

	public void Add(string pluginName, IEnumerable<SimpleEventHandler> handlers)
	{
		_handlers.Add(pluginName, handlers.ToList());
		Rebuild();
	}

	public void Invoke(EventInput input)
		=> _currentRegistry.Invoke(input);

}
