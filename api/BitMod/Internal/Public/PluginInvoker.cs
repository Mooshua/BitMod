using BitMod.Plugins.Events;

using Lilikoi.Context;

using Serilog;

namespace BitMod.Internal.Public;

public class PluginInvoker : Mount
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

	public TResult Produce<TEventArgs, TResult>(TEventArgs args, Func<TResult> defaultValue)
		where TEventArgs : class
		where TResult : class
	{
		var chain = _context.Producers.Get(typeof(TEventArgs));
		var result = chain.Invoke(EventInput.From(args));

		TResult value = result.As <TResult>() ?? defaultValue();

		var mutatorChain = _context.Mutator.Get(typeof(TEventArgs));
		mutatorChain.Invoke(EventInput.From(args, value));

		return value;
	}

	public TResult Produce<TEventArgs, TResult>(TEventArgs args, TResult defaultValue)
		where TEventArgs : class
		where TResult : class
		=> Produce(args, () => defaultValue);
}
