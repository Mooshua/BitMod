using BitMod.Flags.Configuration;
using BitMod.Internal;

using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

namespace BitMod.Flags.Attribute.Wraps;

public class FlagSwitchAttribute : LkTypedWrapAttribute<EventInput, Task>
{
	public FlagSwitchAttribute(string name, bool defaultBehavior)
	{
		_name = name;
		_defaultBehavior = defaultBehavior;
	}

	private string _name;
	private bool _defaultBehavior;

	public override WrapResult<Task> Before(Mount context, ref EventInput input)
	{
		if (!context.Has<FlagFile>())
			throw new InvalidOperationException("BitMod context does not have flag object! Is the flag plugin loaded?");

		var flagFile = context.Get<FlagFile>();
		var flagGroup = flagFile.Get(input?.Server?.ToString());

		var symbol = flagGroup.Fetch(_name, _defaultBehavior ? "yes" : "no");

		if (symbol.AsBoolean == false)
			return WrapResult<Task>.Stop(Task.CompletedTask);

		return WrapResult<Task>.Continue();
	}

	public override void After(Mount mount, ref Task output)
	{

	}
}
