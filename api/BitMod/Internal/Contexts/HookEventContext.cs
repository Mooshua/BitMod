using BitMod.Internal.Handlers;
using BitMod.Internal.Registries;
using BitMod.Plugins.Events;

namespace BitMod.Internal.Contexts;

internal class HookEventContext
{
	private readonly Dictionary<string, List<HookEventHandler>> _handlers = new Dictionary<string, List<HookEventHandler>>();

	private HookEventRegistry _currentRegistry;

	public HookEventContext()
	{
		_currentRegistry = new HookEventRegistry(new List<HookEventHandler>());
	}

	private void Rebuild()
	{
		_currentRegistry = new HookEventRegistry(_handlers
			.SelectMany(kv => kv.Value)
			.ToList());
	}

	public void Remove(string pluginName)
	{
		_handlers.Remove(pluginName);
		Rebuild();
	}

	public void Add(string pluginName, IEnumerable<HookEventHandler> handlers)
	{
		_handlers.Add(pluginName, handlers.ToList());
		Rebuild();
	}

	public Directive Invoke(EventInput input)
		=> _currentRegistry.Invoke(input);

}
