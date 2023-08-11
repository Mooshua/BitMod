using System.Reflection;

namespace BitMod.Events.Meta;

public class PluginUnloadEvent
{
	public PluginUnloadEvent(Assembly assembly, string name)
	{
		PluginAssembly = assembly;
		Name = name;
	}

	public string Name { get; }

	public Assembly PluginAssembly { get; }
}
