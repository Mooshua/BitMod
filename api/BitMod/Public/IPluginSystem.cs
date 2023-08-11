using BitMod.Plugins;

namespace BitMod.Public;

public interface IPluginSystem : IBitModComponent
{

	IReadOnlyList<IPlugin> Loaded { get; }

	void Unload(IPlugin plugin);

	void Stop();

}
