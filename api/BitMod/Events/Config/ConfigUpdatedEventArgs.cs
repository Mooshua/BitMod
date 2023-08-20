using BitMod.Configuration.Model;
using BitMod.Events.Base;

namespace BitMod.Events.Config;

public class ConfigUpdatedEventArgs : IBaseArgs
{
	public ConfigUpdatedEventArgs(string file, IConfigObject config)
	{
		File = file;
		Config = config;
	}

	/// <summary>
	/// The name of the file (relative to /configs) that updated.
	/// </summary>
	public string File { get; }

	/// <summary>
	/// The updated copy of the config object.
	/// </summary>
	public IConfigObject Config { get; }

}
