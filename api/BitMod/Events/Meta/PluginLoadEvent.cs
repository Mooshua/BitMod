using System.Reflection;

using BitMod.Events.Base;

using Lilikoi.Context;

namespace BitMod.Events.Meta;

/// <summary>
/// Called when a plugin is loaded into the
/// </summary>
public class PluginLoadEvent : IEventArgs
{
	public PluginLoadEvent(Assembly pluginAssembly, string name)
	{
		PluginAssembly = pluginAssembly;
		Name = name;
	}

	public string Name { get; }

	/// <summary>
	/// The assembly that was loaded
	/// </summary>
	public Assembly PluginAssembly { get; }
}
