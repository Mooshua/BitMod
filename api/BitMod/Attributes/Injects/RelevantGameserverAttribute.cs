using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Internal;

using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

namespace BitMod.Attributes.Injects;

public class RelevantGameserverAttribute : LkTypedParameterAttribute<GameServer, EventInput>
{
	public override GameServer Inject(Mount context, EventInput input)
	{
		if (input.RelevantGameServer is not null)
			return input.RelevantGameServer;

		throw new InvalidOperationException("No relevant gameserver found!");
	}
}
