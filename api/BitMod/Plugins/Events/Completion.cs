namespace BitMod.Plugins.Events;

public enum Completion
{
	/// <summary>
	/// This invocation completes the request,
	/// no further invocations are necessary.
	/// </summary>
	Completed,

	/// <summary>
	/// This invocation completes the request,
	/// but keep executing anyways for whatever reason.
	/// </summary>
	Peek,

	/// <summary>
	/// This invocation does not complete the request.
	/// </summary>
	Continue,
}
