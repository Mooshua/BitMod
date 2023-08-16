using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using BitMod.Events.Base;
using BitMod.Internal.Handlers;
using BitMod.Plugins.Extensions;
using BitMod.Router;

using Lilikoi.Compiler.Public;
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

	private BitMod _env { get; }

	private List<BaseRouter> _routers { get; } = new List<BaseRouter>();

	public PluginContext(BitMod env)
	{
		_logger = env.Logger;
		_env = env;
	}

	public BaseRouter? Router<TResult, TSearch>()
	{
		foreach (BaseRouter baseRouter in _routers)
		{
			if (baseRouter.Is<TResult, TSearch>())
				return baseRouter;
		}
		return null;
	}

	public TResult? Get<TResult, TSearch>(TSearch search)
		where TResult : class
	{
		var router = Router<TResult, TSearch>();

		if (router == null)
			return null;

		return router.As<TResult, TSearch>()?.Find(search);
	}

	internal Router<TResult, TSearch> Router<TResult, TSearch>(Func<IRouteAssembler<TResult, TSearch>> ctor)
		where TResult : class
	{
		var found = this.Router<TResult, TSearch>();

		if (found != null)
			return found.As<TResult, TSearch>()!;

		var newRouter = new Router<TResult,TSearch>( ctor() );
		_routers.Add(newRouter);
		return newRouter;
	}

	private void Register(List<LilikoiContainer> containers)
	{
		foreach (LilikoiContainer lilikoiContainer in containers)
		{
			if (lilikoiContainer.Has<RouterAssignments>())
			{
				var assignments = lilikoiContainer.Get<RouterAssignments>();
				assignments!.Register(lilikoiContainer);
				//_logger.Debug("[BitMod PluginContext] Assigning {@Assignments} to container {@Container}.", assignments, lilikoiContainer);
			}
		}
	}

	private IEnumerable<IExtension> GetExtensions(Assembly assembly)
	{
		foreach (Type type in assembly.GetTypes())
		{
			if (typeof(IExtension).IsAssignableFrom(type))
				yield return Activator.CreateInstance(type) as IExtension;
		}
	}

	/// <summary>
	/// Load a single type in with the unique name "name"
	/// </summary>
	/// <param name="name"></param>
	/// <param name="type"></param>
	public void Load(string name, Type type)
	{
		var context = new RouterContext(_env, name);
		var containers = Scanner.Scan<RouterContext, EventInput, Task>(context, type, () => _env);

		_logger.Debug("[PluginContext] Found {@ContainerCount} containers in type {@Type}", containers.Count, type.FullName);

		Register(containers);
	}

	/// <summary>
	/// Load a whole assembly with the unique name "name"
	/// </summary>
	/// <param name="name"></param>
	/// <param name="assembly"></param>
	public void Load(string name, Assembly assembly)
	{
		var context = new RouterContext(_env, name);
		var containers = Scanner.Scan<RouterContext, EventInput, Task>(context, assembly, () => _env);

		_logger.Debug("[PluginContext] Found {@ContainerCount} containers in assembly {@Assembly}", containers.Count, assembly.FullName);

		Register(containers);
	}

	/// <summary>
	/// Unload an assembly and completely remove it's hooks
	/// </summary>
	/// <param name="name"></param>
	public void Unload(string name)
	{
		foreach (BaseRouter baseRouter in _routers)
		{
			baseRouter.Remove(name);
		}
	}
}
