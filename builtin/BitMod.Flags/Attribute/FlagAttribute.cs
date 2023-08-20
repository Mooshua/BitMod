using BitMod.Configuration.Model;
using BitMod.Flags.Configuration;
using BitMod.Internal;
using BitMod.Public;

using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

namespace BitMod.Flags.Attribute;

public class FlagAttribute : LkTypedParameterAttribute<IConfigSymbol, EventInput>
{
	private string _name;
	private string _fallback;

	public FlagAttribute(string name, string fallback)
	{
		_name = name;
		_fallback = fallback;
	}

	public override IConfigSymbol Inject(Mount context, EventInput input)
	{
		if (!context.Has<FlagFile>())
			throw new InvalidOperationException("BitMod context does not have flag object! Is the flag plugin loaded?");

		var flagFile = context.Get<FlagFile>();
		var flagGroup = flagFile.Get(input?.Server?.ToString());

		return flagGroup.Fetch(_name, _fallback);
	}
}
