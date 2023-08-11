using System.Reflection;

using BitMod.Plugins;
using BitMod.Plugins.Extensions;

namespace BitMod.Public;

public interface IPluginSystem : IBitModComponent
{
	public interface IPlugin
	{
		public string Name { get; }

		public Assembly Assembly { get; }
	}

	IReadOnlyList<IPlugin> Plugins { get; }

	/// <summary>
	/// Extensions are logical ways of keeping track of any
	/// components added to the root mount.
	/// </summary>
	IReadOnlyList<IExtension> Extensions { get; }

	/// <summary>
	/// Request the plugin system initialize this extension
	/// </summary>
	/// <param name="extension"></param>
	void Load(IExtension extension);

	/// <summary>
	/// As this extension to uninitialize itself
	/// </summary>
	/// <param name="extension"></param>
	void Unload(IExtension extension);

	void Stop();

}
