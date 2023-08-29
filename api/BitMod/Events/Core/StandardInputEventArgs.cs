using BitMod.Events.Base;

namespace BitMod.Events.Core;

public class StandardInputEventArgs : IEventArgs
{
	public StandardInputEventArgs(string contents)
	{
		Contents = contents;
	}

	public string Contents { get; }
}
