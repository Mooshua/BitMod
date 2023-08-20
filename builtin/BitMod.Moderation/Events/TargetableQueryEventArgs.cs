using BitMod.Compatibility;
using BitMod.Events.Base;

namespace BitMod.Moderation.Events;

public class TargetableQueryEventArgs : IHookArgs
{

	/// <summary>
	/// The person who would be executing the command,
	/// if allowed.
	/// </summary>
	public BitPlayer Executor { get; }

	/// <summary>
	/// The victim that will be targeted
	/// </summary>
	public BitPlayer Victim { get; }

}
