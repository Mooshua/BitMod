using BitMod.Internal.Handlers;
using BitMod.Internal.Registries;
using BitMod.Plugins.Events;

namespace BitMod.Internal.Contexts;

internal class ProducerEventContext
{
	private readonly Dictionary<string, List<ProducerEventHandler>> _handlers = new Dictionary<string, List<ProducerEventHandler>>();

	private ProducerEventRegistry _currentRegistry;

	public ProducerEventContext()
	{
		_currentRegistry = new ProducerEventRegistry(new List<ProducerEventHandler>());
	}

	private void Rebuild()
	{
		_currentRegistry = new ProducerEventRegistry(_handlers
			.SelectMany(kv => kv.Value)
			.ToList());
	}

	public void Remove(string pluginName)
	{
		_handlers.Remove(pluginName);
		Rebuild();
	}

	public void Add(string pluginName, IEnumerable<ProducerEventHandler> handlers)
	{
		_handlers.Add(pluginName, handlers.ToList());
		Rebuild();
	}

	public Product Invoke(EventInput input)
		=> _currentRegistry.Invoke(input);

}
