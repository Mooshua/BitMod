using System.Reflection;

using BitMod.Events.Base;

namespace BitMod.Events.Meta;

public class PluginUnloadEvent : IEventArgs
{
	public PluginUnloadEvent(Assembly assembly, string name)
	{
		PluginAssembly = assembly;
		Name = name;
	}

	public string Name { get; }

	public Assembly PluginAssembly { get; }
}
