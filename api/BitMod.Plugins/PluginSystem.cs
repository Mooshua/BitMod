using System.Reflection;

using BitMod.Events.Meta;
using BitMod.Internal.Public;
using BitMod.Plugins.Extensions;
using BitMod.Public;

using McMaster.NETCore.Plugins;

namespace BitMod.Plugins;

public class PluginSystem : IPluginSystem
{
	public class Plugin : IPluginSystem.IPlugin
	{
		public Plugin(string name, Assembly assembly, PluginLoader loader)
		{
			Name = name;
			Assembly = assembly;
			Loader = loader;
		}

		public string Name { get; }

		public Assembly Assembly { get; }

		public PluginLoader Loader { get; }
	}

	private BitMod _parent;

	public PluginSystem()
	{
	}

	private PluginInvoker _invoker;

	private PluginContext _context;

	private PluginWatcher _watcher;

	public void Start(BitMod env)
	{
		_parent = env;
		_invoker = _parent.Invoker;
		_context = _parent.Context;
		_watcher = new PluginWatcher(this, env.Logger);
	}

	private List<Plugin> _plugins = new List<Plugin>();

	private List<IExtension> _extensions = new List<IExtension>();

	public IReadOnlyList<IPluginSystem.IPlugin> Plugins => _plugins;

	public IReadOnlyList<IExtension> Extensions => _extensions;

	public void Load(IExtension extension)
	{
		_parent.Logger.Information("[BitMod Plugins] Loading extension {@Name}", extension.Name);

		if (_extensions.Contains(extension))
			return;

		extension.Register(_parent);
		_extensions.Add(extension);
	}

	public void Unload(IExtension extension)
	{
		_parent.Logger.Information("[BitMod Plugins] Unloading extension {@Name}", extension.Name);

		extension.Unregister(_parent);
		_extensions.Remove(extension);
	}

	internal void Load(string name, Assembly plugin, PluginLoader loader)
	{
		_parent.Logger.Information("[BitMod Plugins] Loading plugin {@Name} ({@Assembly})", name, plugin.FullName);

		var obj = new Plugin(name,plugin, loader);
		var eventArg = new PluginLoadEvent(plugin, name);

		_plugins.Add(obj);
		_invoker.Event(eventArg);
	}

	internal void Unload(string name)
	{
		var plugin = _plugins.First(plugin => plugin.Name.Equals(name));

		_parent.Logger.Information("[BitMod Plugins] Unloading plugin {@Name} ({@Assembly})", name, plugin.Assembly.FullName);
		var eventArg = new PluginUnloadEvent(plugin.Assembly, name);

		_invoker.Event(eventArg);

		plugin.Loader.Dispose();
		_plugins.RemoveAll(existing => existing.Name == name);
	}

	public void Stop()
	{
		throw new NotImplementedException();
	}
}
