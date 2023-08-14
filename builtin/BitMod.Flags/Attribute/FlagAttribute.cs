using System.Reflection;

using BitMod.Events.Accessors;
using BitMod.Flags.Configuration;
using BitMod.Public;

using Lilikoi.Attributes;
using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

using Serilog;

namespace BitMod.Flags.Attribute;

public class FlagAttribute : LkTypedParameterAttribute<FlagEntry, IGameserverEvent>
{
	public string _value;

	public FlagAttribute(string value = "#default")
	{
		_value = value;
	}

	public override FlagEntry Inject(Mount context, IGameserverEvent input)
	{
		if (!context.Has<FlagFile>())
		{
			var config = context.Get<IConfigurationSystem>();
			var file = config.Get("flags", () => new Dictionary<string, FlagFileEntry>());
			var parsed = new FlagFile(file, context.Get<ILogger>()!);

			context.Store(parsed);
		}

		var flagfile = context.Get<FlagFile>();

		var gameserver = input?.Server;
		if (gameserver != null)
			return flagfile.Get($"{gameserver.GameIP.ToString()}:{gameserver.GamePort.ToString()}").Get(_value);

		return flagfile.Get("#default").Get(_value);
	}
}
