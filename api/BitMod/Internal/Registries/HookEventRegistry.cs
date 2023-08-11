using BitMod.Internal.Handlers;
using BitMod.Plugins.Events;

namespace BitMod.Internal.Registries;

internal class HookEventRegistry
{
	public HookEventRegistry(List<HookEventHandler> children)
	{
		children.Sort((a,b) => a.Priority.CompareTo( b.Priority ));
		Children = children;
	}

	public List<HookEventHandler> Children { get; }

	public Directive Invoke(EventInput input)
	{
		foreach (HookEventHandler hookEvent in Children)
		{
			var hookResult = hookEvent.Invoke(input);

			if (hookResult == Directive.Allow)
				return hookResult;

			if (hookResult == Directive.Disallow)
				return hookResult;
		}

		//	Entire chain is completely neutral.
		return Directive.Neutral;
	}
}
