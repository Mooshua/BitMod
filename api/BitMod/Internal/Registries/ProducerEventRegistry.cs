using System;
using System.Collections.Generic;
using System.Linq;

using BitMod.Internal.Handlers;
using BitMod.Plugins.Events;

using Serilog;

namespace BitMod.Internal.Registries;

internal class ProducerEventRegistry
{
	public ProducerEventRegistry(List<ProducerEventHandler> children, ILogger logger)
	{
		Children = children
			.OrderByDescending(ev => ev.Priority)
			.ToList();
		_logger = logger;
	}

	private ILogger _logger;

	public List<ProducerEventHandler> Children { get; }

	public Product Invoke(EventInput input)
    {
    	foreach (ProducerEventHandler producerEvent in Children)
    	{
	        try
	        {
		        var product = producerEvent.Invoke(input);

		        if (product.Exists)
			        return product;
	        }
	        catch (Exception ex)
	        {
		        _logger.Error(ex, "Producer failed during execution!");
		        if (Environment.DoNotCatchEventExceptions)
			        throw;
	        }
        }

		//	Entire chain is useless and produced nothing.
        return Product.None();
    }
}
