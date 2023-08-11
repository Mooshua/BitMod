using BitMod.Internal.Handlers;
using BitMod.Plugins.Events;

namespace BitMod.Internal.Registries;

internal class ProducerEventRegistry
{
	public ProducerEventRegistry(List<ProducerEventHandler> children)
	{
		children.Sort((a,b) => a.Priority.CompareTo( b.Priority ));
		Children = children;
	}

	public IReadOnlyCollection<ProducerEventHandler> Children { get; }

	public Product Invoke(EventInput input)
    {
    	foreach (ProducerEventHandler producerEvent in Children)
    	{
    		var product = producerEvent.Invoke(input);

            if (product.Exists)
	            return product;
        }

		//	Entire chain is useless and produced nothing.
        return Product.None();
    }
}
