using BitMod.Plugins.Events;

using Serilog;

namespace BitMod.Internal.Public;

public class PluginInvoker
{
	private PluginContext _context;

	public PluginInvoker(PluginContext context)
	{
		_context = context;
	}

	public void Event<TEventArgs>(TEventArgs args) where TEventArgs : class
	{
		var chain = _context.Simple.Get(typeof(TEventArgs));
		chain.Invoke( EventInput.From(args) );
	}

	public bool Hook<TEventArgs>(TEventArgs args, bool defaultValue = false) where TEventArgs : class
	{
		var chain = _context.Hooks.Get(typeof(TEventArgs));
		var result = chain.Invoke( EventInput.From(args) );

		if (result == Directive.Allow)
			return true;

		if (result == Directive.Disallow)
			return false;

		return defaultValue;
	}
}
