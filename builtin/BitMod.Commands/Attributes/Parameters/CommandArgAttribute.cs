using BitMod.Commands.Handlers;
using BitMod.Internal;

using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

namespace BitMod.Commands.Attributes.Parameters;

public class CommandArgAttribute : LkTypedParameterAttribute<CommandArg, EventInput>
{

	private int _index;

	public CommandArgAttribute(int index)
	{
		_index = index;
	}

	public override CommandArg Inject(Mount context, EventInput input)
	{
		var command = input.Get<CommandInput>();

		if (command.Arguments.Length <= _index)
			//	If no arg exists, return an empty arg, so the
			//	container can choose how to handle it.
			return new CommandArg();

		return new CommandArg(command.Arguments[_index]);
	}
}
