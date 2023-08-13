using BitMod.Commands.Handlers;
using BitMod.Commands.Sources;
using BitMod.Internal;

using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

namespace BitMod.Commands.Attributes.Inject;

public class CommandSenderAttribute : LkTypedParameterAttribute<ICommandSource, EventInput>
{
	public override ICommandSource Inject(Mount context, EventInput input)
		=> input.Get<CommandInput>().Sender;
}
