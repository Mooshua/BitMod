using BitMod.Compatibility;
using BitMod.Extensions.Commands.Lilikoi;

using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

namespace BitMod.Extensions.Commands.Attributes;

internal class ImplicitCommandSenderAttribute : LkTypedParameterAttribute<BitPlayer, CommandInput>
{
	public override BitPlayer Inject(Mount context, CommandInput input)
		=> input.Sender;
}
