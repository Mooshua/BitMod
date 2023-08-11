using BitMod.Events;
using BitMod.Internal;

using Lilikoi.Compiler.Public;

namespace BitMod.Attributes.Targets;

public class BitEventAttribute : BitTargetAttribute
{
	public override string Name => "BitEvent";

	internal override bool IsValidEvent(Type arg)
		=> Registry.Events[arg] == Registry.EventType.None;

	internal override void Setup(EventRegistrationContext context, LilikoiMutator mutator)
	{

	}
}
