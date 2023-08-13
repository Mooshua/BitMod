using System;
using System.Reflection;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Internal;

using Lilikoi.Attributes;
using Lilikoi.Attributes.Builders;
using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

namespace BitMod.Attributes.Injects;

public class ResponsiblePlayerAttribute : LkTypedParameterAttribute<BitPlayer, EventInput>
{
	public override BitPlayer Inject(Mount context, EventInput input)
	{
		if (input.ResponsiblePlayer is not null)
			return input.ResponsiblePlayer;

		throw new InvalidOperationException("No responsible player found!");
	}
}
