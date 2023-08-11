using Lilikoi.Context;

namespace BitMod.Plugins.Extensions;

/// <summary>
/// Extensions extend the global plugin context
/// and add additional types to plugins.
///
/// Extensions are extremely powerful and are utilized
/// within BitMod to provide functionality.
/// </summary>
public interface IExtension
{

	string Name { get; }

	/// <summary>
	/// Register this extension into the global application mount.
	/// This makes your types you place here accessible throughout
	/// the application.
	/// </summary>
	/// <param name="mount"></param>
	void Register(Mount mount);

	/// <summary>
	/// Un-register this extension. This is called before
	/// a plugin is unloaded.
	/// </summary>
	/// <param name="mount"></param>
	void Unregister(Mount mount);
}
