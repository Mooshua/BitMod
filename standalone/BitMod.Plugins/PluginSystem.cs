using System.Reflection;

using BitMod.Events.Meta;
using BitMod.Internal.Public;
using BitMod.Plugins.Extensions;
using BitMod.Public;

using McMaster.NETCore.Plugins;

namespace BitMod.Plugins;

public class PluginSystem : IPluginSystem
{

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

		try
		{
			extension.Register(_parent);
			_extensions.Add(extension);
		}
		catch (Exception e)
		{
			_parent.Logger.Error(e, "[BitMod Plugins] Failed loading extension {@Name}!", extension.Name);
		}
	}

	public void Unload(IExtension extension)
	{
		_parent.Logger.Information("[BitMod Plugins] Unloading extension {@Name}", extension.Name);

		try
		{
			extension.Unregister(_parent);
		}
		catch (Exception e)
		{
			_parent.Logger.Warning(e, "[BitMod Plugins] Extension {@Name} threw exception while unloading", extension.Name);
		}

		_extensions.Remove(extension);
	}

	internal IEnumerable<Type> ExtensionsIn(Assembly assembly)
		=> assembly.GetTypes()
			.Where(type => typeof(IExtension).IsAssignableFrom(type))
			.ToList();

	internal IEnumerable<IExtension> ExtensionObjectsIn(Assembly assembly)
	{
		var extensions = ExtensionsIn(assembly)
			.ToList();

		foreach (Type type in extensions)
		{
			IExtension instance = null;
			try
			{
				instance = Activator.CreateInstance(type) as IExtension;
			}
			catch (Exception e)
			{
				_parent.Logger.Error(e,"[BitMod Plugins] Error instantiating extension of type {@Type}", type.FullName);
			}

			if (instance != null)
				yield return instance;
		}
	}

	internal void Load(Plugin plugin)
	{
		_parent.Logger.Information("[BitMod Plugins] Loading plugin {@Name} ({@Assembly})", plugin.Name, plugin.Assembly.FullName);

		var eventArg = new PluginLoadEvent(plugin.Assembly, plugin.Name);

		foreach (IExtension extension in ExtensionObjectsIn(plugin.Assembly))
			Load(extension);

		_plugins.Add(plugin);
		_context.Load(plugin.Name, plugin.Assembly);
		_invoker.Event(eventArg);
	}

	internal void Unload(Plugin plugin)
	{
		_parent.Logger.Information("[BitMod Plugins] Unloading plugin {@Name} ({@Assembly})", plugin.Name, plugin.Assembly.FullName);
		var eventArg = new PluginUnloadEvent(plugin.Assembly, plugin.Name);

		foreach (IExtension extension in ExtensionObjectsIn(plugin.Assembly))
			Unload(extension);

		_invoker.Event(eventArg);
		_context.Unload(plugin.Name);

		if (_plugins.Contains(plugin))
			_plugins.Remove(plugin);
	}

	internal void Deleted(string name)
	{
		foreach (Plugin plugin1 in _plugins.Where(plugin => plugin.Name == name))
		{
			plugin1.UnloadPlugin();
		}
	}

	public void Stop()
	{
		throw new NotImplementedException();
	}
}
