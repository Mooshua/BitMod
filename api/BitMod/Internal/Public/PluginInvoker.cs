using System;

using BitMod.Configuration.Model;
using BitMod.Events.Base;
using BitMod.Events.Config;
using BitMod.Internal.Registries;
using BitMod.Plugins.Events;

using Lilikoi.Context;

using Serilog;

namespace BitMod.Internal.Public;

public class PluginInvoker
{
	public PluginContext Context;

	public PluginInvoker(PluginContext context)
	{
		Context = context;
	}

	public void Event<TEventArgs>(TEventArgs args)
		where TEventArgs : class, IEventArgs
	{
		var chain = Context.Get<SimpleEventRegistry, Type>(typeof(TEventArgs));
		chain?.Invoke( EventInput.From(args) );
	}

	public void ConfigUpdate(string fileName, IConfigObject updated)
	{
		var chain = Context.Get<ConfigUpdatedRegistry, string>(fileName);
		chain?.Invoke( EventInput.From( new ConfigUpdatedEventArgs(fileName, updated), updated) );
	}

	public bool Hook<TEventArgs>(TEventArgs args, bool defaultValue = false)
		where TEventArgs : class, IHookArgs
	{
		var chain = Context.Get<HookEventRegistry, Type>(typeof(TEventArgs));
		var result = chain?.Invoke( EventInput.From(args) ) ?? Directive.Neutral;

		if (result == Directive.Allow)
			return true;

		if (result == Directive.Disallow)
			return false;

		return defaultValue;
	}

	public TResult Produce<TEventArgs, TResult>(TEventArgs args, Func<TResult> defaultValue)
		where TEventArgs : class, IProducerArgs<TResult>
		where TResult : class
	{
		var chain = Context.Get<ProducerEventRegistry, Type>(typeof(TEventArgs));
		var result = chain?.Invoke(EventInput.From(args));

		TResult value = result?.As <TResult>() ?? defaultValue();

		var mutatorChain = Context.Get<MutatorEventRegistry, Type>(typeof(TEventArgs));
		mutatorChain?.Invoke(EventInput.From(args, value));

		return value;
	}

	public TResult Produce<TEventArgs, TResult>(TEventArgs args, TResult defaultValue)
		where TEventArgs : class, IProducerArgs<TResult>
		where TResult : class
		=> Produce(args, () => defaultValue);
}
