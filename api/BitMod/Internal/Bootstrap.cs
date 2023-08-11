using BitMod.Attributes.Targets;
using BitMod.Events.Meta;
using BitMod.Internal.Public;
using BitMod.Plugins.Extensions;
using BitMod.Public;

using Lilikoi.Standard;

using Serilog;

namespace BitMod.Internal;

public class Bootstrap
{
	[Singleton]
	private PluginContext _context;

	[Singleton]
	private IPluginSystem _pluginSystem;

	[Singleton]
	private ILogger _logger;

	[BitEvent]
	public Task OnPluginLoad(PluginLoadEvent ev)
	{
		_logger.Information("[BitMod Bootstrap] Loading plugin {@Name}", ev.Name);
		_context.Load(ev.Name, ev.PluginAssembly);

		foreach (Type type in ev.PluginAssembly.GetTypes())
		{
			if (type.IsAssignableTo(typeof(IExtension)))
			{
				_logger.Debug("[BitMod Bootstrap] Found extension {@Name}", type.FullName);
				var instance = Activator.CreateInstance(type);
				_pluginSystem.Load(instance as IExtension);
			}
		}
		return Task.CompletedTask;
	}

	[BitEvent]
	public Task OnPluginUnload(PluginUnloadEvent ev)
	{
		_logger.Information("[BitMod Bootstrap] Unloading plugin {@Name}", ev.Name);
		_context.Unload(ev.Name);

		foreach (Type type in ev.PluginAssembly.GetTypes())
		{
			if (type.IsAssignableTo(typeof(IExtension)))
			{
				var instance = Activator.CreateInstance(type);
				_pluginSystem.Unload(instance as IExtension);
			}
		}
		return Task.CompletedTask;
	}
}
