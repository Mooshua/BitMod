﻿using System.Reflection;

using BitMod.Internal.Handlers;
using BitMod.Internal.LilikoiRouting;
using BitMod.Internal.Registries;

using Lilikoi.Compiler.Public;
using Lilikoi.Context;
using Lilikoi.Scan;

using Serilog;

namespace BitMod.Internal.Contexts;

internal class SimpleEventContext
{
	private readonly Dictionary<string, List<SimpleEventHandler>> _handlers = new Dictionary<string, List<SimpleEventHandler>>();

	private SimpleEventRegistry _currentRegistry;

	private ILogger _logger;

	public SimpleEventContext(ILogger logger)
	{
		_logger = logger;
		_currentRegistry = new SimpleEventRegistry(new List<SimpleEventHandler>(), _logger);
	}

	private void Rebuild()
	{
		_currentRegistry = new SimpleEventRegistry(_handlers
			.SelectMany(kv => kv.Value)
			.ToList(), _logger);
	}

	public void Remove(string pluginName)
	{
		_handlers.Remove(pluginName);
		Rebuild();
	}

	public void Add(string pluginName, IEnumerable<SimpleEventHandler> handlers)
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


	public void Invoke(EventInput input)
		=> _currentRegistry.Invoke(input);

	public static List<LilikoiContainer> Scan(EventRegistrationContext ctx, MethodInfo methodInfo, Func<Mount> mount)
		=> Scanner.Scan<SimpleRegistrationContext, EventInput, Task>(new SimpleRegistrationContext(ctx), methodInfo, mount);
}
