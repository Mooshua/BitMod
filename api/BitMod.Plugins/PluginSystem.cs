using BitMod.Internal.Public;

namespace BitMod.Plugins;

public class PluginSystem
{
	public PluginSystem(PluginContext context)
	{
		Invoker = new PluginInvoker(context);
		Context = context;
	}

	public PluginInvoker Invoker { get; }
	public PluginContext Context { get; }
}
