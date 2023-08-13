using System;

using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Internal;

using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

namespace BitMod.Attributes.Injects;

public class RelevantGameserverAttribute : LkTypedParameterAttribute<BitServer, EventInput>
{
	public override BitServer Inject(Mount context, EventInput input)
	{
		if (input.Server is not null)
			return input.Server;

		throw new InvalidOperationException("No relevant gameserver found!");
	}
}
