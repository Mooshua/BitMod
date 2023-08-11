using System.Reflection;

using BitMod.Internal.Contexts;
using BitMod.Internal.Handlers;
using BitMod.Internal.Routing;

using Lilikoi.Context;
using Lilikoi.Scan;

using Serilog;

namespace BitMod.Internal.Public;

/// <summary>
/// APIs for plugin loaders to load plugins into BitMod
/// </summary>
public class PluginContext
{
	private ILogger _logger;

	public PluginContext(ILogger logger)
	{
		_logger = logger;
	}

	public Mount Global { get; } = new Mount();

	internal Router< HookEventContext > Hooks { get; } = new ();

	internal Router< ProducerEventContext > Producers { get; } = new ();

	internal Router< SimpleEventContext > Simple { get; } = new ();

	public void Load(string name, MethodInfo method)
	{
		try
		{
			var ctx = new EventRegistrationContext();

			var hooks = HookEventContext.Scan(ctx, method, () => Global);
			var producers = ProducerEventContext.Scan(ctx, method, () => Global);
			var simple = SimpleEventContext.Scan(ctx, method, () => Global);

			if (ctx.Event == null)
			{
				_logger.Verbose("Loading {@FuncName} encountered no matching events!", method.Name);
				return;
			}

			Hooks.Get(ctx.Event).Add(name, hooks.Select(hook => new HookEventHandler(hook)));
			Producers.Get(ctx.Event).Add(name, producers.Select(hook => new ProducerEventHandler(hook)));
			Simple.Get(ctx.Event).Add(name, simple.Select(hook => new SimpleEventHandler(hook)));

			_logger.Debug("Loaded {@FuncName} with type {@EvType}: {@Hooks} hooks, {@Producers} producers, {@Simple} simple",
				method.Name, ctx.Event.FullName, hooks.Count, producers.Count, simple.Count);
		}
		catch (Exception exception)
		{
			_logger.Error(exception, "Failed loading in {@MethodName} from {@Class}", method.Name, method.DeclaringType.FullName);
		}
	}

	/// <summary>
	/// Load a single type in with the unique name "name"
	/// </summary>
	/// <param name="name"></param>
	/// <param name="type"></param>
	public void Load(string name, Type type)
	{
		foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			Load(name, methodInfo);
	}

	/// <summary>
	/// Load a whole assembly with the unique name "name"
	/// </summary>
	/// <param name="name"></param>
	/// <param name="assembly"></param>
	public void Load(string name, Assembly assembly)
	{
		foreach (Type type in assembly.GetTypes())
			Load(name, type);
	}

	/// <summary>
	/// Unload an assembly and completely remove it's hooks
	/// </summary>
	/// <param name="name"></param>
	public void Unload(string name)
	{
		foreach (var context in Hooks.All())
			context.Remove(name);

		foreach (var context in Simple.All())
			context.Remove(name);

		foreach (var context in Producers.All())
			context.Remove(name);
	}
}
