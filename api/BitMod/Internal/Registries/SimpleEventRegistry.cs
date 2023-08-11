using BitMod.Internal.Handlers;

namespace BitMod.Internal.Registries;

internal class SimpleEventRegistry
{
	public SimpleEventRegistry(List<SimpleEventHandler> children)
	{
		Children = children;
	}

	public List<SimpleEventHandler> Children { get; }

	public void Invoke(EventInput input)
	{
		foreach (SimpleEventHandler eventHandler in Children)
			eventHandler.Invoke(input);
	}
}
