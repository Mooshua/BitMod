using BitMod.Commands.Handlers;
using BitMod.Internal;

using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

namespace BitMod.Commands.Attributes.Parameters;

public class CommandInputAttribute : LkTypedParameterAttribute<CommandInput, EventInput>
{
	public override CommandInput Inject(Mount context, EventInput input)
		=> input.Get<CommandInput>();
}
