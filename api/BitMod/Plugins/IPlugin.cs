namespace BitMod.Plugins;

public interface IPlugin
{
	/// <summary>
	/// The fancy name of this plugin
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// This is you, bucko.
	/// </summary>
	public string Author { get; }

	/// <summary>
	/// The version. You should update this every now and then,
	/// as it may be used for determining compatibilities.
	/// </summary>
	public Version Version { get; }

	/// <summary>
	/// Called as the plugin is loading
	/// </summary>
	/// <returns></returns>
	public Task Loaded();

	/// <summary>
	/// Called before the plugin is hot-reloaded
	/// </summary>
	/// <returns></returns>
	public Task Reloading();

	/// <summary>
	/// Called before the plugin is gracefully unloaded
	/// </summary>
	/// <returns></returns>
	public Task Unloading();
}
