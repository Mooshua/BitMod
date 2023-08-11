namespace BitMod.Plugins.Events;

public enum Directive
{
	/// <summary>
	/// This plugin does not have an opinion on the direction to take
	/// </summary>
	Neutral,

	/// <summary>
	/// The plugin wishes to disallow the action from happening
	/// </summary>
	Disallow,

	/// <summary>
	/// The plugin wishes to allow the action
	/// </summary>
	Allow,
}
