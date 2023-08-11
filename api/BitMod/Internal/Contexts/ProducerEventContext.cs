using System.Reflection;

using BitMod.Internal.Handlers;
using BitMod.Internal.LilikoiRouting;
using BitMod.Internal.Registries;
using BitMod.Plugins.Events;

using Lilikoi.Compiler.Public;
using Lilikoi.Context;
using Lilikoi.Scan;

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
		if (_handlers.ContainsKey(pluginName))
		{
			var existingHandlers = _handlers[pluginName];
			existingHandlers.AddRange(handlers);
		}
		else
		{
			_handlers.Add(pluginName, handlers.ToList());
		}

		Rebuild();
	}

	public Product Invoke(EventInput input)
		=> _currentRegistry.Invoke(input);

	public static List<LilikoiContainer> Scan(EventRegistrationContext ctx, MethodInfo methodInfo, Func<Mount> mount)
		=> Scanner.Scan<ProducerRegistrationContext, EventInput, Task<Product>>( new ProducerRegistrationContext( ctx ), methodInfo, mount);

}
