namespace BitMod.Flags.Configuration;

/// <summary>
/// The groups that compose the flag file
/// </summary>
public class FlagFileEntry
{

	/// <summary>
	/// A list of groups that this flag group inherits
	/// </summary>
	public List<string> Inherit { get; set; } = new ();

	/// <summary>
	/// A list of flags that this group sets, and their values
	/// </summary>
	public Dictionary<string, string> Set { get; set; } = new ();
}
